import CommunicationManager from "../Managers/CommunicationManager.js"
class InstrumentsController {
    /**
     * fetches global instruments from the server
     * */
    getAllInstruments() {
        return CommunicationManager.sendServer("/api/instruments", null, "GET");
    }
    /**
     * returns an array of instrument IDs taken from a dom element
     * @param {DOM element} view
     * @returns {Array}
     */
    getInstrumentIds(view) {
        let inputElements = view.querySelectorAll(".instrument [name='instrumentId']");
        let instruemtsIds = [];
        inputElements.forEach(el => {
            if (!isNaN(el.value))
                instruemtsIds.push(parseInt(el.value))
        });
        return instruemtsIds;
    }
    /**
     * filters a dom element containing instruments according to a search term
     * @param {DOM object} view
     * @param {string} searchTerm
     */
    searchInstrumentList(view,searchTerm) {
        let elements = view.querySelectorAll(".instrument");
        elements.forEach(el => {
            if (el.innerText.indexOf(searchTerm) === -1) {
                el.setAttribute("hidden", "hidden");
            }
            else el.removeAttribute("hidden");
        });
    }
}
export default new InstrumentsController();

