import ViewManager from "./ViewManager.js";
import MainPageVC from "../ViewControllers/MainPageVC.js";
import LoginVC from "../ViewControllers/LoginVC.js";
import RegisterVC from "../ViewControllers/RegisterVC.js";
class NavigationManager {
    constructor() {
        window.history.replaceState("", null, "");
        window.onpopstate = (event) => {
            if (event.state) {
                this.goToPage(event.state);
            } 
        };
    }
    goToLogin() {
        let view = LoginVC.renderLogin(null);
        moveToPage("login",view);
    }
    goToRegister() {
        let view = RegisterVC.renderRegisterPage();
        moveToPage("register",view);
    }
    goToMainPage(user, allInstruments) {
        let view = MainPageVC.renderMain(user, allInstruments);
        moveToPage("main",view);

    }
    goToPage(pageName) {
        switch (pageName) {
            case "login":
                this.goToLogin();
                break;
            case "register":
                this.goToRegister();
                break;
            case "main":
                this.goToMainPage();
                break;
        }
    }
}
export default new NavigationManager();

/**
 * Set the active view on the page and adds the state to the window's history
 * @param {string} pageName
 * @param {DOM object} view
 */
function moveToPage(pageName, view) {
    window.history.pushState(pageName, pageName);
    ViewManager.setAppView(view);
}
