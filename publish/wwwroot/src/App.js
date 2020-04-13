import UsersController from "./Controllers/UsersController.js";
import NavigationManager from "./Managers/NavigationManager.js";
class App {
    init() {
        
        UsersController.loadUser();

    }
}
export default new App();










