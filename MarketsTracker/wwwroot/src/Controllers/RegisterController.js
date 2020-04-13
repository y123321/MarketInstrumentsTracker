import CommunicationManager from "../Managers/CommunicationManager.js";
import NavigationManager from "../Managers/NavigationManager.js";
import RegisterVC from "../ViewControllers/RegisterVC.js";
class RegisterController {
    register(user) {
        return CommunicationManager.sendServer("/api/users", user, "POST", NavigationManager.goToLogin, false, false)
            .catch(e => {
                if (e.json) {
                    e.json().then(err => {
                        if (err.errors)
                            RegisterVC.setValidationErrors(err.errors);
                    });
                }
                console.log(e);
            });
    }

}
export default new RegisterController();