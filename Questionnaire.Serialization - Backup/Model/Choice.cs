using Questionnaire.Data;
using Questionnaire.Serialization.Attributes;

namespace Questionnaire.Serialization.Model
{
    [LocalizationTargetType(typeof(ValueTextPair))]
    public class Choice : IValueTextPair
    {
        [LocalizableString("Text")]
        public string text { get; set; }

        public bool ShouldSerializetext()
        {
            return !string.IsNullOrEmpty(this.text);
        }

        [LocalizableString("Value")]
        public string value { get; set; }

        public bool ShouldSerializevalue()
        {
            return !string.IsNullOrEmpty(this.value);
        }
    }
}
