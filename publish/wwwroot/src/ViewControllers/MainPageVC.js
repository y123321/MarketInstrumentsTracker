import UserVc from "./UserVC.js";
import ViewManager from "../Managers/ViewManager.js";
import InstrumentsVC from "./InstrumentsVC.js";
import UsersController from "../Controllers/UsersController.js";
import CommunicationManager from "../Managers/CommunicationManager.js";
import NavigationManager from "../Managers/NavigationManager.js";
import InstrumentsController from "../Controllers/InstrumentsController.js";

const userInsrumntsSelector = ".userInstruments";
const globalInstumentsSelector = ".globalInstruments";

class MainPageVC {
    static CONTAINER_TEMPLATE = ViewManager.createTemplate("mainPageTemplate");
    renderMain(user, allInstruments) {
        if (!user)
            return null;
        if (!allInstruments)
            allInstruments = [];
        if (!user.instruments)
            user.instruments = [];

        let container = MainPageVC.CONTAINER_TEMPLATE.cloneNode(true);

        addUserData(user, container);

        addUserInstruments(user, container);

        addGlobalInstruments(container, user, allInstruments);

        bindElements(container, user);

        return container;
    }

}
export default new MainPageVC();
function addGlobalInstruments(container, user, allInstruments) {
    let globalInstrumentsEl = container.querySelector(globalInstumentsSelector);
    var userInstrumentsIndex = {};
    user.instruments.forEach(i => userInstrumentsIndex[i.instrumentId] = true);
    let instruments = allInstruments.filter(i => !userInstrumentsIndex[i.instrumentId]);
    let instrumentsView = InstrumentsVC.renderInstrumentsArr(instruments);
    globalInstrumentsEl.appendChild(instrumentsView);
}

function addUserInstruments(user, container) {
    let userInstrumentsView = InstrumentsVC.renderInstrumentsArr(user.instruments, user.userId);
    var instrumentsContainer = container.querySelector(userInsrumntsSelector);
    instrumentsContainer.appendChild(userInstrumentsView);
}

function addUserData(user, container) {
    let userView = UserVc.renderUser(user);
    let userContainer = container.querySelector(".userContainer");
    userContainer.appendChild(userView);
}

function bindElements(container, user) {
    bindSaveBtn(container, user);
    bindAddInsrumentBtn(container);
    bindDeleteInsrumentBtn(container);
    bindLogOut(container);
    bindSelectableListItems(container);
    bindGlobalSearchBar(container);
    bindUserSearchBar(container);
}
function bindGlobalSearchBar(container) {
    let searchBar = container.querySelector(".globalSearchBar")
    let instsList = container.querySelector(globalInstumentsSelector);
    bindSearchBar(searchBar, instsList);
}
function bindSearchBar(searchBar, instsList) {
    searchBar.addEventListener("keyup", () => InstrumentsController.searchInstrumentList(instsList, searchBar.value));
    searchBar.addEventListener("search", () => InstrumentsController.searchInstrumentList(instsList, searchBar.value));
}

function bindUserSearchBar(container) {
    let searchBar = container.querySelector(".userSearchBar")
    let instsList = container.querySelector(userInsrumntsSelector);
    bindSearchBar(searchBar, instsList);
}

function bindSelectableListItems(container) {
    container.querySelectorAll(".selectable")
        .forEach(e => e.addEventListener("click",
            () => e.classList.toggle("active")));
}
function bindSaveBtn(container, user) {
    let btn = container.querySelector(".btnSaveInstruments");
    let resultMsgEl = container.querySelector(".resultMsg");
    let clearMsgHandler = ()=>setTimeout(() => resultMsgEl.innerHTML = "", 3000)
    let updateHandler = () => {
        resultMsgEl.class = "txt-succuss";
        resultMsgEl.innerHTML = "Success";
        clearMsgHandler();
    };
    let failureHandler = (e) => {
        console.log(e);
        resultMsgEl.class = "txt-danger";
        resultMsgEl.innerHTML = "Failure";
        clearMsgHandler();
    };
    btn.addEventListener("click", () => UsersController.saveUserInstruments(user.userId, updateHandler, failureHandler));
}
function bindAddInsrumentBtn(container) {
    bindMoveInstrumentsBtn(container, ".btnAddInstruments", globalInstumentsSelector, userInsrumntsSelector);
}
function bindDeleteInsrumentBtn(container) {
    bindMoveInstrumentsBtn(container, ".btnDelete", userInsrumntsSelector, globalInstumentsSelector);
}
function bindMoveInstrumentsBtn(container, btnSelector, listFromSelecor, listToContinerSelector) {
    let btn = container.querySelector(btnSelector);
    let handler = () => {
        let instsToMove = container.querySelectorAll(listFromSelecor + " .instrument.active:not([hidden])");
        let listTo = container.querySelector(listToContinerSelector + " " + InstrumentsVC.instrumentListSelector);
        instsToMove.forEach(el => {
            el.classList.toggle("active");
            if (listTo.firstChild !== null)
                listTo.insertBefore(el, listTo.firstChild);
            else listTo.appendChild(el);
        });
    };
    btn.addEventListener("click", handler);
}
function bindLogOut(container) {
    var btn = container.querySelector(".btnLogOut");
    btn.addEventListener("click", () => {
        CommunicationManager.setSecurity(" ", " ");
        NavigationManager.goToLogin();
    });
}
