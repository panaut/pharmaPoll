using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class Survey
    {
        public string surveyId { get; set; }

        public string surveyPostId { get; set; }

        public string clientId { get; set; }

        public string cookieName { get; set; }

        public bool sendResultOnPageNext { get; set; } = false;

        public string commentPrefix { get; set; } = "-Comment";

        public string title { get; set; }

        public bool focusFirstQuestionAutomatic { get; set; } = true;

        public bool showNavigationButtons { get; set; } = true;

        public bool showPageTitles { get; set; } = true;

        public bool showCompletedPage { get; set; } = true;

        public string completedHtml { get; set; }

        public string requiredText { get; set; } = "*";

        public string questionStartIndex { get; set; }

        public string questionTitleTemplate { get; set; }

        public string showProgressBar { get; set; } = "off";

        public bool storeOthersAsComment { get; set; } = true;

        public bool goNextPageAutomatic { get; set; } = false;

        public IEnumerable<Page> pages { get; set; } = new List<Page>();

        public IEnumerable<SurveyTrigger> triggers { get; set; } = new List<SurveyTrigger>();

        public bool clearInvisibleValues { get; set; } = false;

        public Page currentPageValue { get; set; }

        public Hashtable valuesHash { get; set; }

        public Hashtable variablesHash { get; set; }

        public string pagePrevTextValue { get; set; }

        public string pageNextTextValue { get; set; }

        public string completeTextValue { get; set; }

        public bool showPageNumbersValue { get; set; } = false;

        public string showQuestionNumbersValue { get; set; } = "on";

        public string questionTitleLocationValue { get; set; } = "top";

        public string localeValue { get; set; }

        public bool isCompleted { get; set; } = false;

        public bool isLoading { get; set; } = false;

        public Hashtable processedTextValues { get; set; }

        public TextPreProcessor textPreProcessor { get; set; }

        public bool isValidatingOnServerValue { get; set; } = false;

        public string modeValue { get; set; } = "edit";

        public bool isDesignModeValue { get; set; } = false;
    }
}
