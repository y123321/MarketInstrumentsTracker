import NavigationManager from "../Managers/NavigationManager.js";
import ViewManager from "../Managers/ViewManager.js";
import LoginController from "../Controllers/LoginController.js";

class LoginVC  {
    renderLogin(loginData) {
        let view = ViewManager.renderModelObject(loginData, "loginTemplate");
        let loginBtn = view.querySelector(".btnLogin");
        loginBtn.addEventListener("click", () => {
            let data = ViewManager.serializeToObj(view);
            LoginController.login(data);
        });
        let registerBtn = view.querySelector(".btnGoToRegister");
        registerBtn.addEventListener("click", NavigationManager.goToRegister);
        return view;
    }
}
export default new LoginVC();