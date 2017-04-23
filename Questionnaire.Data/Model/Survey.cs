using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model
{
    [Table("Surveys")]
    public class Survey
    {
        [JsonPropertyAttribute(PropertyName = "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public bool IsActive { get; set; } = false;

        public bool ShouldSerializeIsActive()
        {
            return false;
        }

        public string SurveyJson { get; set; }

        public bool ShouldSerializeSurveyJson()
        {
            return false;
        }

        public bool clearInvisibleValues { get; set; } = false;

        public bool ShouldSerializeclearInvisibleValues()
        {
            return this.clearInvisibleValues;
        }

        [MaxLength(1024)]
        public string completeText { get; set; }

        public bool ShouldSerializecompleteText()
        {
            return !string.IsNullOrEmpty(this.completeText);
        }

        public string completedHtml { get; set; }

        public bool ShouldSerializecompletedHtml()
        {
            return !string.IsNullOrEmpty(this.completedHtml);
        }

        [MaxLength(256)]
        public string cookieName { get; set; }

        public bool ShouldSerializecookieName()
        {
            return !string.IsNullOrEmpty(this.cookieName);
        }

        public bool focusFirstQuestionAutomatic { get; set; } = true;

        public bool ShouldSerializefocusFirstQuestionAutomatic()
        {
            return !this.focusFirstQuestionAutomatic;
        }

        public bool goNextPageAutomatic { get; set; } = false;

        public bool ShouldSerializegoNextPageAutomatic()
        {
            return this.goNextPageAutomatic;
        }

        [MaxLength(8)]
        public string locale { get; set; }

        public bool ShouldSerializelocale()
        {
            return !string.IsNullOrEmpty(this.locale);
        }

        [MaxLength(16)]
        public string mode { get; set; } = "edit";

        public bool ShouldSerializemode()
        {
            return this.mode != "edit";
        }

        [MaxLength(32)]
        public string pageNextText { get; set; }

        public bool ShouldSerializepageNextText()
        {
            return !string.IsNullOrEmpty(this.pageNextText);
        }

        [MaxLength(32)]
        public string pagePrevText { get; set; }

        public bool ShouldSerializepagePrevText()
        {
            return !string.IsNullOrEmpty(this.pagePrevText);
        }

        [MaxLength(32)]
        public string questionStartIndex { get; set; }

        public bool ShouldSerializequestionStartIndex()
        {
            return !string.IsNullOrEmpty(this.questionStartIndex);
        }

        [MaxLength(16)]
        public string questionTitleLocationValue { get; set; } = "top";

        public bool ShouldSerializequestionTitleLocationValue()
        {
            return this.questionTitleLocationValue != "top";
        }

        [MaxLength(256)]
        public string questionTitleTemplate { get; set; }

        public bool ShouldSerializequestionTitleTemplate()
        {
            return !string.IsNullOrEmpty(this.questionTitleTemplate);
        }

        [MaxLength(32)]
        public string requiredText { get; set; } = "*";

        public bool ShouldSerializerequiredText()
        {
            return this.requiredText != "*";
        }

        public bool sendResultOnPageNext { get; set; } = false;

        public bool ShouldSerializesendResultOnPageNext()
        {
            return this.sendResultOnPageNext;
        }

        public bool showCompletedPage { get; set; } = true;

        public bool ShouldSerializeshowCompletedPage()
        {
            return !this.showCompletedPage;
        }

        public bool showNavigationButtons { get; set; } = true;

        public bool ShouldSerializeshowNavigationButtons()
        {
            return !this.showNavigationButtons;
        }

        public bool showPageNumbers { get; set; } = false;

        public bool ShouldSerializeshowPageNumbers()
        {
            return this.showPageNumbers;
        }

        public bool showPageTitles { get; set; } = true;

        public bool ShouldSerializeshowPageTitles()
        {
            return !this.showPageTitles;
        }

        [MaxLength(16)]
        public string showProgressBar { get; set; } = "off";

        public bool ShouldSerializeshowProgressBar()
        {
            return this.showProgressBar != "off";
        }

        [MaxLength(16)]
        public string showQuestionNumbers { get; set; } = "on";

        public bool ShouldSerializeshowQuestionNumbers()
        {
            return this.showQuestionNumbers != "on";
        }

        public bool showTitle { get; set; } = true;

        public bool ShouldSerializeshowTitle()
        {
            return !this.showTitle;
        }

        public bool storeOthersAsComment { get; set; } = true;

        public bool ShouldSerializestoreOthersAsComment()
        {
            return !this.storeOthersAsComment;
        }

        [MaxLength(256)]
        public string surveyId { get; set; }

        public bool ShouldSerializesurveyId()
        {
            return !string.IsNullOrEmpty(this.surveyId);
        }

        [MaxLength(256)]
        public string surveyPostId { get; set; }

        public bool ShouldSerializesurveyPostId()
        {
            return !string.IsNullOrEmpty(this.surveyPostId);
        }

        [MaxLength(1024)]
        public string title { get; set; }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        public virtual ICollection<Page> pages { get; set; } = new List<Page>();


        public virtual ICollection<SurveyTrigger> triggers { get; set; } = new List<SurveyTrigger>();

        public bool ShouldSerializetriggers()
        {
            return this.triggers != null && this.triggers.Any();
        }
    }
}
