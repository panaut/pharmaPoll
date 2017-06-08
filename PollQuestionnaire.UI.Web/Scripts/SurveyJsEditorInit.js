var editorOptions = { showJSONEditorTab: true, showEmbededSurveyTab: false };
var survey = new SurveyEditor.SurveyEditor("surveyEditorContainer", editorOptions);

// Add database id property to survey object
Survey.JsonObject.metaData.addProperty("survey", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("survey", { name: "surveyCode:string" });
Survey.JsonObject.metaData.addProperty("page", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("panel", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("questionbase", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("surveyvalidator", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("multipletextitem", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("matrixdropdowncolumn", { name: "internalId:number" });
Survey.JsonObject.metaData.addProperty("checkbox", { name: "renderAs", default: "standard", choices: ["standard", "sortablejs"] });

// Add SortableJs Widget
Survey.CustomWidgetCollection.Instance.addCustomWidget(widgetSortablejs);

survey.onCanShowProperty.add(function (sender, options) {
    if (options.obj.getType() == "survey") {
        options.canShow = options.property.name != "internalId" && options.property.name != "surveyId";
    } else if (options.obj.getType() == "page") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "text") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "checkbox") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "radiogroup") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "dropdown") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "comment") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "rating") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "html") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "file") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "matrix") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "matrixdropdown") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "matrixdynamic") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "multipletext") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    } else if (options.obj.getType() == "panel") {
        options.canShow = options.property.name != "internalId" && options.property.name != "name";
    }
});

survey.render('surveyEditorContainer');