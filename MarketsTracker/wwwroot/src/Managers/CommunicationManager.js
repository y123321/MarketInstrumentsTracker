import NavigationManager from "./NavigationManager.js";
const headers = {
    'Accept': 'application/json',
    "Content-Type": "application/json"
}
const baseUrl = location.origin;
class CommunicationManager {
    constructor() {
        //get persistent auth if available
        this.setSecurity();
    }
    /**
     * sends a request to the web server
     * 
     * @param {String} relativeUrl      should start with a "/"
     * @param {Object} data                should be null for GET requests
     * @param {String} method              GET/POST/DELETE/PUT
     * @param {Function} executer            a delegate function to run after a successful response, accepts the returned object
     * @param {Boolean} isRedirect          when set to true, sends the user to login page if accepted "Unauthorized" response (status 401)
     * @param {Boolean} isDefaultFailure    when set to true show the returned error message on alert. //todo: make it nicer
     */
    sendServer(relativeUrl, data, method, executer, isRedirect = true, isDefaultFailure = true) {

        let requestObj = {
            method: method,
            headers: headers,
        }
        if (!executer)
            executer = (x) => x;
        if (data)
            requestObj["body"] = JSON.stringify(data);
        var onError = null;
        if (isDefaultFailure)
            onError=defaultComunicationError;

        var promise = fetch(baseUrl + relativeUrl, requestObj)
            .then(response => {
                if (response.ok) {
                    let contentType = response.headers.get("content-type");
                    if (contentType && contentType.indexOf("application/json") !== -1)
                        return response.json();
                    else return response.text();
                }
                if (response.status === 401 && isRedirect) {
                    NavigationManager.goToLogin();
                    throw response.statusText;
                }
                else throw response;
            }).catch(err => {
                if (onError)
                    return onError(err);
                else throw err;
            });
        if (executer) {
            promise.then(executer).
                catch(e => console.log(e));
        }
        return promise;
    }

    /**
     * set the security headers that would be used in any future communication
     * the values are saved persistently for a single log in.
     * IMPORTANT! if the values are null, emply, flase etc. the previous security token would be used
     * @param {any} authToken       looks like giberish
     * @param {any} authType        authentication scheme, typically "Bearer"
     */
    setSecurity(authToken, authType) {
        let authTokenName = "AUTH_TOKEN";
        let authTokenType = "AUTH_TYPE";
        let setAuthToken = (value) => localStorage[authTokenName] = value;
        let setAuthType = (value) => localStorage[authTokenType] = value;
        let getAuthToken = () => localStorage[authTokenName];
        let getAuthType = () => localStorage[authTokenType];
        if (authToken)
            setAuthToken(authToken);
        else authToken = getAuthToken();
        if (authType)
            setAuthType(authType);
        else authType = getAuthType();
        if (authToken) {
            headers["Authorization"] = authType + ' ' + authToken;
        }
    }


}
export default new CommunicationManager();

function defaultComunicationError(err) {
    if (typeof (err.message) === "string") {
        console.log(err);
        alert(err.message);
    }
    else {
        var statusText = err.statusText;
        console.log(err);
        if (typeof (err.text) === "function") {
            err.text()
                .then((errText) => {
                    var msg = errText || statusText;
                    console.log(errText);
                    alert(msg);
                });
        }
    }
    return Promise.reject(err);
}