using Newtonsoft.Json;
using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("matrixdropdown")]
    public class QuestionMatrixDropdown : QuestionMatrixDropdownBase
    {
        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }
    }
}
