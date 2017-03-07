function dxBroker() {
    this._urlBase = 'http://dxsurvey.com/api/MySurveys/';
    this._tempAccessKey = '12df74da0f5843fab34ae2655ca0d06c';

    this.getActiveSurveys = function (callback) {
        $.ajax({
            url: this._urlBase + 'getActive',
            method: 'GET',
            async: true,
            data: { accessKey: this._tempAccessKey },
            success: function (data, textStatus, jqXHR) {
                if (callback != undefined) {
                    callback(data, textStatus, jqXHR);
                }
            }
        });
    }

    this.regenerateAccessKey = function (callback) {
        $.ajax({
            url: this._urlBase + 'generateAccessKey',
            method: 'GET',
            async: true,
            data: { accessKey: this._tempAccessKey },
            success: function (data, textStatus, jqXHR) {
                if (callback != undefined) {
                    callback(data, textStatus, jqXHR);
                }
            }
        });
    }

    this.createNewSurvey = function (surveyName, callback) {
        var survey = null;

        $.ajax({
            url: this._urlBase + 'create',
            method: 'GET',
            async: true,
            data: { accessKey: this._tempAccessKey, name: surveyName },
            success: function (data, textStatus, jqXHR) {
                if (callback != undefined) {
                    callback(data, textStatus, jqXHR);
                }
            }
        })

        return this;
    }

    this.changeSurvey = function (id, survey, callback) {
        $.ajax({
            url: this._urlBase + 'changeJson?accessKey=__accessKey__'.replace('__accessKey__', this._tempAccessKey),
            method: 'POST',
            async: true,
            crossDomain: true,
            data: { Id: id, Json: survey, Text: survey },
            dataType: 'json',
            complete: function (surveyId, jqXHR, textStatus) {
                if (callback != undefined) {
                    callback(id, jqXHR, textStatus);
                }
            }
        });
    }

    this.publishSurvey = function (id, callback) {
        $.ajax({
            url: this._urlBase + 'publish/__id__'.replace('__id__', id),
            method: 'GET',
            async: true,
            data: { accessKey: this._tempAccessKey, generateNewId: false },
            success: function (data, textStatus, jqXHR) {
                if (callback != undefined) {
                    callback(id, data, textStatus, jqXHR);
                }
            }
        });
    }

    this.setUseCookies = function (id, useCookie, callback) {
        $.ajax({
            url: this._urlBase + 'setUseCookies/__id__'.replace('__id__', id),
            method: 'GET',
            async: true,
            data: { accessKey: this._tempAccessKey, useCookies: useCookie },
            success: function (data, textStatus, jqXHR) {
                if (callback != undefined) {
                    callback(id, data, textStatus, jqXHR);
                }
            }
        });
    }
}