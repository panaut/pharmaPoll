var widgetSortablejs = {
    name: "sortablejs",
    isFit: function (question) { return question["renderAs"] === 'sortablejs'; },
    htmlTemplate: `<div></div>`,
    afterRender: function (question, el) {
        var $el = $(el);

        $el.append(
            `<div class="row" style="margin: 0">
              <div class ="result sortable-container col-xs-12 col-sm-7 col-md-5 col-lg-4"></div>
            </div>`);

        var $result = $el.find(".result");
        var $emptyText = $result.find("span");

        var index = 1;
        question.visibleChoices.forEach(function (choice) {
            var itemEl = $result.append(`<div data-id="`
                + choice.value
                + `" class="row">
                <div class ="btn btn-default btn-block sortable-item" style="padding:0">
                    <table style="border-style: hidden">
                        <tr style="border-style: hidden"><td rowspan="2" class ="col-xs-1"><span class="badge" id="revIndex">`
                        + index++ +
                        `</span></td><td rowspan="2" class="col-xs-10" id="elementTitle" title="`
                        + choice.text
                        + `"><div class="truncate-div"><div>`
                        + choice.text
                        + `</div></div></td><td class ="col-xs-1"><span class ="glyphicon glyphicon-chevron-up" style="font-size: 0.8em;vertical-align:bottom;"></span></td></tr>
                        <tr style="border-style: hidden"><td class="col-xs-1"><span class="glyphicon glyphicon-chevron-down" style="font-size: 0.8em;vertical-align:top;"></span></td></tr>
                    </table>
                </div>
            </div>`);
        });

        $el.append(`<div class="row" style="margin-right: 30pt"><div class="col-xs-12 col-sm-7 col-md-5 col-lg-4"><button class="btn btn-default pull-right confirmation">`
            + localizedStrings.sortableWidget_confirm
            + `</button></div></div>`);

        $el.find(".confirmation").click(prepareResult);

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
                    for (var i = 0; i < evt.to.children.length; i++) {
                        $(evt.to.children[i]).find("span#revIndex").html(i + 1);
                        result.push(evt.to.children[i].dataset.id)
                    }
                }
                question.value = result;
            }
        });

        function prepareResult(btnClickEvent) {
            var initialResult = [];
            for (var i = 0; i < sortableChoices.toArray().length; i++) {
                initialResult.push(sortableChoices.toArray()[i])
            }

            question.value = initialResult;
        }
    }
}