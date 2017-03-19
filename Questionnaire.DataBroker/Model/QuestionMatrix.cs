using Newtonsoft.Json;
using Questionnaire.DataBroker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("matrix")]
    public class QuestionMatrix : QuestionBaseSelect
    {
        public bool isAllRowRequired { get; set; } = false;

        public IEnumerable<MatrixDropdownColumn> columns { get; set; }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }
    }
}
