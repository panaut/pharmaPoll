using Questionnaire.Serialization.Attributes;

namespace Questionnaire.Serialization.Model
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
