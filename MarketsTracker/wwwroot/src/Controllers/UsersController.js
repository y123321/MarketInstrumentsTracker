import CommunicationManager from "../Managers/CommunicationManager.js"
import NavigationManager from "../Managers/NavigationManager.js"
import InstrumentsController from "./InstrumentsController.js";
import ViewManager from "../Managers/ViewManager.js";
import InstrumentsVC from "../ViewControllers/InstrumentsVC.js";
class UsersController {
    /**
     * Feth user data from the server and go to main page.
     * If the user is not authenticated he would be redirected to login
     * */
    loadUser() {
        var executer =(user) => InstrumentsController.getAllInstruments()
            .then(instruments => NavigationManager.goToMainPage(user, instruments));
        //go to the the "users" action and gets redirected to the apropriate user id
        //useful for making administrator capabillity in the future
        return CommunicationManager.sendServer("/api/users", null, "GET", executer, true, false);
            
    }

    /**
     * gets an array of ids of the user's instruments
     * @param {int} userId
     */
    getUserInstrumentsIds(userId) {
        let viewId = InstrumentsVC.getViewCollectionId(userId);;
        let view = ViewManager.getView(viewId);
        let instruemtsIds = InstrumentsController.getInstrumentIds(view);
        return instruemtsIds;
    }
    /**
     * seves the current user's instruments list in the server
     * @param {any} userId
     * @param {any} updateHandler
     * @param {any} failureHandler
     */
    saveUserInstruments(userId, updateHandler, failureHandler) {
        let instrumentIds = this.getUserInstrumentsIds(userId);
        return CommunicationManager.sendServer("/api/users/" + userId + "/instruments", instrumentIds, "PUT", updateHandler, false, false)
            .catch(failureHandler);
    }

}
export default new UsersController();