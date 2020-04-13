class ViewsManager {
    constructor() {
        this.appView = document.getElementById("app");
    }
    
    /**
     * display the view on the page and remove the current view
     * @param {DOM object} view
     */
    setAppView(view) {
        this.appView.innerHTML = "";
        this.appView.appendChild( view)
    }

    /**
     * Clones a dom elements from the content of an element in the HTML page
     * pay attention the element returned would ONLY BE THE FIRST ELEMENT contained in the element selected
     * meant to work with <script type="text/template"> tags
     * @param {any} templateId      the Id attribute of the template on the webpage
     * @returns {DOM object}        a clone of the selected template
     */
    createTemplate (templateId) {
        let templateRef = document.getElementById(templateId);
        let template = document.importNode(templateRef, true);
        if (!template)
            throw "template with the id:" + templateId + " does not exist"
        var el = document.createElement('div');
        el.innerHTML=template.innerHTML.trim();
        return el.firstChild;
    }

    /**
    * set values o single property in a model object to a signgke DOM element in template 
    * with a "name" attribute matchin to the prop name
    * @param {DOM element} template
    * @param {model object} obj
    * @param {string} propName
    */
    setTemplateValue(template, obj, propName) {
        if (typeof (template) !== "object" || typeof (template.querySelector) !== "function")
            throw "template parameter is not a DOM element";
        if (typeof (obj) !== "object" || typeof (obj[propName]) === "undefined")
            return;
        let el = template.querySelector("[name='" + propName + "']");
        if (el === null)
            return;
        if (el.nodeName === "input" || el.nodeName === "INPUT") {
            el.value = obj[propName];
        }
        else {
            el.innerHTML = obj[propName];
        }
    }
    /**
    * set values from a model object to a DOM element template
    * each property in the object would be assinged to an element with
    * a corresponding "name" attribute
    * @param {model object} obj
    * @param {DOM element} template
    * @param {string} viewId
    */
    setTemplateFieldsValues(obj, template, viewId) {
        if (obj) {
            for (let prop in obj)
                this.setTemplateValue(template, obj, prop);
        }
        template.setAttribute("data-id", viewId);
    }

    /**
     * gets a dom element by a hook that was set with the function "setTemplateFieldsValues"
     * @param {any} viewId          hook
     * @returns {DOM element}       the view that was hooked
     */
    getView(viewId) {
        var view = this.appView.querySelector("[data-id='" + viewId + "']");
        return view;
    }


    /**
     * returns a clone of the selected template populated by a model object 
     * @param {object} obj              Model object to set values from
     * @param {string} templateId       Id property of templateId dom element
     * @returns {DOM element}           a view, which is a populated template
     */
    renderModelObject(obj, templateId, viewId) {
        //object must be object, false or undefined
        if (obj && typeof (obj) !== "object")
            throw "parameter is not an object";
        if (!templateId || typeof (templateId) !== "string")
            throw "templateId must have a non-emply string value";
        let templateStr = document.querySelector("#" + templateId).innerHTML;
        var template = document.createElement("div");
        template.innerHTML = templateStr;
        template=template.firstElementChild;
        template.setAttribute("id", "");
        //fill template if object has data, otherwise keep it empty
        this.setTemplateFieldsValues(obj, template, viewId);
        return template;
    }

    /**
     * @param  {Node}   form The form to serialize
     * @return {String}      The serialized form data
     */
    serializeToObj (form) {

        let obj = {};
        // Loop through each field in the form
        for (var i = 0; i < form.elements.length; i++) {
            var field = form.elements[i];
            // Don't serialize fields without a name, submits, buttons, file and reset inputs, and disabled fields
            if (!field.name || field.disabled || field.type === 'file' || field.type === 'reset' || field.type === 'submit' || field.type === 'button')
                continue;
            // If a multi-select, get all selections
            if (field.type === 'select-multiple') {
                for (var n = 0; n < field.options.length; n++) {
                    if (!field.options[n].selected)
                        continue;
                    obj[field.name] = field.options[n].value;
                }
            }

            // Convert field data to a query string
            else if ((field.type !== 'checkbox' && field.type !== 'radio') || field.checked) {
                obj[field.name] = field.value;
            }
        }

        return obj;

    }

}
export default new ViewsManager();