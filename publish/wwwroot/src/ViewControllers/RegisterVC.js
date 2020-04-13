import ViewManager from "../Managers/ViewManager.js";
import RegisterController from "../Controllers/RegisterController.js";

class RegisterVC {
    _registrationView=null;
    renderRegisterPage() {
        var view = ViewManager.renderModelObject(null, "registrationTemplate");
        this._registrationView = view;
        let registerBtn = view.querySelector(".btnRegister");
        registerBtn.addEventListener("click", () => {
            let data = ViewManager.serializeToObj(view);
            RegisterController.register(data);
        });
        return view;
    }
    setValidationErrors(errors) {
        if (!errors)
            return;
        if (this._registrationView !== null) {
            let el = this._registrationView.querySelector(".validationErrors");
            let msg = "";
            for (let prop in errors) {
                msg += prop + ": " + errors[prop]+"<br/>";
            }
            el.innerHTML = msg;
        }
    }
}
export default new RegisterVC();