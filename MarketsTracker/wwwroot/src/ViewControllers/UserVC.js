import ViewManager from "../Managers/ViewManager.js";

class UserVc {
    static CONTAINER_TEMPLATE = ViewManager.renderModelObject(null,"userViewTemplate")
    renderUser(user) {
        let view = UserVc.CONTAINER_TEMPLATE.cloneNode(true);
        let viewId = this.getViewId(user.userId);
        ViewManager.setTemplateFieldsValues(user, view, viewId);
        return view;
    }
    getViewId(userId) {
        return "user_" + userId;
    }
    
}
export default new UserVc();