import CommunicationManager from "../Managers/CommunicationManager.js";
import UserController from "./UsersController.js";

class LoginController {
    login(data) {
        let executer = loginRes => {
            if (loginRes) {
                //apply authorization token recieved from the server
                CommunicationManager.setSecurity(loginRes.token, loginRes.scheme);
            }
            else {
                //use stored login data
                CommunicationManager.setSecurity();

            } 
            //after user is authenticated, load the data from the server
            UserController.loadUser();
        }
        return CommunicationManager.sendServer("/api/login", data, "POST", executer);
    }
    loadLoginPage() {
        return CommunicationManager.sendServer("/api/login", null, "GET");
    }

}
export default new LoginController();