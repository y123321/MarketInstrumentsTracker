import ViewManager from "../Managers/ViewManager.js";
const defaultCollectionId = "global";
class InstrumentsVC{
    instrumentListSelector = ".instrumentsList";
    static CONTAINER_TEMPLATE = ViewManager.createTemplate("instrumentsListTemplate");
    renderInstrumentsArr(instruments, collectionId) {
        if (!Array.isArray(instruments))
            return null;
        //get global instruments if no id was selected
        if (!collectionId)
            collectionId = defaultCollectionId;

        let container = getEmpltyContainerTemplate(collectionId);

        addInstrumentsToContainer(container, instruments);

        return container;
    }


    renderInstrument(instrument) {
        var veiwId = this.getViewId(instrument.instrumentId);
        let view = ViewManager.renderModelObject(instrument, "instrumentTemplate", veiwId);
        return view;
    }
    getViewId(instrumentId) {
        return "instrument_" + instrumentId;
    }
    getViewCollectionId(collectionId) {
        if (!collectionId)
            collectionId = defaultCollectionId;
        return "instrument-collection_" + collectionId;
    }
}
const instance = new InstrumentsVC();
export default instance;

function addInstrumentsToContainer(container,instruments) {
    var list = container.querySelector(instance.instrumentListSelector)
    for (let i = 0; i < instruments.length; i++) {
        let instrument = instruments[i];
        if (!instrument.instrumentId)
            continue;
        let view = instance.renderInstrument(instrument);
        list.appendChild(view);
    }
    return list;
}

function getEmpltyContainerTemplate(collectionId) {
    var viewId = instance.getViewCollectionId(collectionId);
    let container = InstrumentsVC.CONTAINER_TEMPLATE.cloneNode(true);
    ViewManager.setTemplateFieldsValues(null, container, viewId);
    return container;
}