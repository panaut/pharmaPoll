using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PollQuestionnaire.UI.Web.Models
{
    public class SupportedLanguageModel
    {
        private string defaultTitle;

        private IDictionary<string, string> surveySpecificTitles = new Dictionary<string, string>();

        public string SurveyCode { get; set; }

        public string Culture { get; set; }

        public CultureInfo CultureInfo { get; set; }

        public SupportedLanguageModel(string culture, CultureInfo ci, string title)
        {
            this.Culture = culture;
            this.CultureInfo = ci;
            this.defaultTitle = title;
        }

        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(this.SurveyCode) || (surveySpecificTitles == null || !surveySpecificTitles.ContainsKey(SurveyCode.ToLower())))
                {
                    return this.defaultTitle;
                }

                return surveySpecificTitles[this.SurveyCode.ToLower()];
            }
            set
            {
                if (string.IsNullOrEmpty(this.SurveyCode.ToLower()))
                {
                    this.defaultTitle = value;
                    return;
                }

                if (!this.surveySpecificTitles.ContainsKey(this.SurveyCode.ToLower()))
                {
                    this.surveySpecificTitles.Add(SurveyCode.ToLower(), value);
                }
                else
                {
                    surveySpecificTitles[this.SurveyCode.ToLower()] = value;
                }
            }
        }

        public SupportedLanguageModel AddSurveySpecificTitle(string srvCode, string title)
        {
            if (!this.surveySpecificTitles.ContainsKey(srvCode.ToLower()))
            {
                this.surveySpecificTitles.Add(srvCode.ToLower(), title);
            }
            else
            {
                surveySpecificTitles[srvCode.ToLower()] = title;
            }

            return this;
        }
    }
}