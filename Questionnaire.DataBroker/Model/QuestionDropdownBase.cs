namespace Questionnaire.DataBroker.Model
{
    public class QuestionDropdownBase : QuestionBaseSelect
    {
        public string optionsCaption { get; set; }

        public bool ShouldSerializeoptionsCaption()
        {
            return !string.IsNullOrEmpty(this.optionsCaption);
        }
    }
}
