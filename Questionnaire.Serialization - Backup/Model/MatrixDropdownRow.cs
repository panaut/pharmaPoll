using Questionnaire.Data;
using Questionnaire.Serialization.Attributes;

namespace Questionnaire.Serialization.Model
{
    [LocalizationTargetType(typeof(MatrixRowTemplate))]
    public class MatrixDropdownRow : IValueTextPair
    {
        [LocalizableString("Value")]
        public string value { get; set; }

        [LocalizableString("Text")]
        public string text { get; set; }
    }
}
