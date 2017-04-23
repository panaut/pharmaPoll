using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    public class Survey
    {
        public string surveyId { get; set; }

        public bool ShouldSerializesurveyId()
        {
            return !string.IsNullOrEmpty(this.surveyId);
        }

        public string surveyPostId { get; set; }

        public bool ShouldSerializesurveyPostId()
        {
            return !string.IsNullOrEmpty(this.surveyPostId);
        }

        public string clientId { get; set; }

        public bool ShouldSerializeclientId()
        {
            return !string.IsNullOrEmpty(this.clientId);
        }

        public string cookieName { get; set; }

        public bool ShouldSerializecookieName()
        {
            return !string.IsNullOrEmpty(this.cookieName);
        }

        public bool sendResultOnPageNext { get; set; } = false;

        public bool ShouldSerializesendResultOnPageNext()
        {
            return this.sendResultOnPageNext;
        }

        public string commentPrefix { get; set; } = "-Comment";

        public bool ShouldSerializecommentPrefix()
        {
            return this.commentPrefix != "-Comment";
        }

        public string title { get; set; }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        public bool focusFirstQuestionAutomatic { get; set; } = true;

        public bool ShouldSerializefocusFirstQuestionAutomatic()
        {
            return !this.focusFirstQuestionAutomatic;
        }

        public bool showNavigationButtons { get; set; } = true;

        public bool ShouldSerializeshowNavigationButtons()
        {
            return !this.showNavigationButtons;
        }

        public bool showPageTitles { get; set; } = true;

        public bool ShouldSerializeshowPageTitles()
        {
            return !this.showPageTitles;
        }

        public bool showCompletedPage { get; set; } = true;

        public bool ShouldSerializeshowCompletedPage()
        {
            return !this.showCompletedPage;
        }

        public string completedHtml { get; set; }

        public bool ShouldSerializecompletedHtml()
        {
            return !string.IsNullOrEmpty(this.completedHtml);
        }

        public string requiredText { get; set; } = "*";

        public bool ShouldSerializerequiredText()
        {
            return this.requiredText != "*";
        }

        public string questionStartIndex { get; set; }

        public bool ShouldSerializequestionStartIndex()
        {
            return !string.IsNullOrEmpty(this.questionStartIndex);
        }

        public string questionTitleTemplate { get; set; }

        public bool ShouldSerializequestionTitleTemplate()
        {
            return !string.IsNullOrEmpty(this.questionTitleTemplate);
        }

        public string showProgressBar { get; set; } = "off";

        public bool ShouldSerializeshowProgressBar()
        {
            return this.showProgressBar != "off";
        }

        public bool storeOthersAsComment { get; set; } = true;

        public bool ShouldSerializestoreOthersAsComment()
        {
            return !this.storeOthersAsComment;
        }

        public bool goNextPageAutomatic { get; set; } = false;

        public bool ShouldSerializegoNextPageAutomatic()
        {
            return this.goNextPageAutomatic;
        }

        public IEnumerable<Page> pages { get; set; } = new List<Page>();
        
        public bool ShouldSerializepages()
        {
            return this.pages != null && this.pages.Any();
        }

        public IEnumerable<SurveyTrigger> triggers { get; set; } = new List<SurveyTrigger>();

        public bool ShouldSerializetriggers()
        {
            return this.triggers != null && this.triggers.Any();
        }

        public bool clearInvisibleValues { get; set; } = false;

        public bool ShouldSerializeclearInvisibleValues()
        {
            return this.clearInvisibleValues;
        }

        public Page currentPageValue { get; set; }

        public bool ShouldSerializecurrentPageValue()
        {
            return this.currentPageValue != null;
        }

        public Hashtable valuesHash { get; set; }

        public bool ShouldSerializevaluesHash()
        {
            return this.valuesHash != null;
        }

        public Hashtable variablesHash { get; set; }

        public bool ShouldSerializevariablesHash()
        {
            return this.variablesHash != null;
        }

        public string pagePrevTextValue { get; set; }

        public bool ShouldSerializepagePrevTextValue()
        {
            return !string.IsNullOrEmpty(this.pagePrevTextValue);
        }

        public string pageNextTextValue { get; set; }

        public bool ShouldSerializepageNextTextValue()
        {
            return !string.IsNullOrEmpty(this.pageNextTextValue);
        }

        public string completeTextValue { get; set; }

        public bool ShouldSerializecompleteTextValue()
        {
            return !string.IsNullOrEmpty(this.completeTextValue);
        }

        public bool showPageNumbersValue { get; set; } = false;

        public bool ShouldSerializeshowPageNumbersValue()
        {
            return this.showPageNumbersValue;
        }

        public string showQuestionNumbersValue { get; set; } = "on";

        public bool ShouldSerializeshowQuestionNumbersValue()
        {
            return this.showQuestionNumbersValue != "on";
        }

        public string questionTitleLocationValue { get; set; } = "top";

        public bool ShouldSerializequestionTitleLocationValue()
        {
            return this.questionTitleLocationValue != "top";
        }

        public string localeValue { get; set; }

        public bool ShouldSerializelocaleValue()
        {
            return !string.IsNullOrEmpty(this.localeValue);
        }

        public bool isCompleted { get; set; } = false;

        public bool ShouldSerializeisCompleted()
        {
            return this.isCompleted;
        }

        public bool isLoading { get; set; } = false;

        public bool ShouldSerializeisLoading()
        {
            return this.isLoading;
        }

        public Hashtable processedTextValues { get; set; }

        public bool ShouldSerializeprocessedTextValues()
        {
            return this.processedTextValues != null;
        }

        public TextPreProcessor textPreProcessor { get; set; }

        public bool ShouldSerializetextPreProcessor()
        {
            return this.textPreProcessor != null; 
        }

        public bool isValidatingOnServerValue { get; set; } = false;

        public bool ShouldSerializeisValidatingOnServerValue()
        {
            return this.isValidatingOnServerValue;
        }

        public string modeValue { get; set; } = "edit";

        public bool ShouldSerializemodeValue()
        {
            return this.modeValue != "edit";
        }

        public bool isDesignModeValue { get; set; } = false;

        public bool ShouldSerializeisDesignModeValue()
        {
            return this.isDesignModeValue;
        }
    }
}
