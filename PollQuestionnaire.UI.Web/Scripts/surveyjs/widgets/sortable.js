var widgetSortablejs = {
    name: "sortablejs",
    isFit: function (question) { return question["renderAs"] === 'sortablejs'; },
    htmlTemplate: `<div></div>`,
    afterRender: function (question, el) {
        var $el = $(el);
        $el.append(`
        <div style="width:50%">
          <div class ="result sortable-container">
          </div>
        </div>
      `);
        var $result = $el.find(".result");
        var $emptyText = $result.find("span");
        question.visibleChoices.forEach(function (choice) {
            $result.append(`<div data-id="` + choice.value + `">
                               <div class ="btn btn-default btn-block sortable-item"> ` + choice.text + ` </div>
                             </div>`);
        });

        var sortableChoices = Sortable.create($result[0], {
            animation: 150,
            group: {
                name: 'top3',
                pull: true,
                put: true
            },
            onSort: function (evt) {
                var result = [];
                if (evt.to.children.length === 1) {
                    $emptyText.css({ display: "inline-block" });
                } else {
                    $emptyText.css({ display: "none" });
                    for (var i = 1; i < evt.to.children.length; i++) {
                        result.push(evt.to.children[i].dataset.id)
                    }
                }
                question.value = result;
            }
        });

        var initialResult = [];
        for (var i = 1; i < sortableChoices.toArray().length; i++) {
            initialResult.push(sortableChoices.toArray()[i])
        }

        question.value = initialResult;
    }
}