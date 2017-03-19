using Questionnaire.DataBroker.Attributes;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("dropdown")]
    public class QuestionDropdown : QuestionBaseSelect
    {
        public string optionsCaption { get; set; }

        public bool ShouldSerializeoptionsCaption()
        {
            return !string.IsNullOrEmpty(this.optionsCaption);
        }
    }
}
