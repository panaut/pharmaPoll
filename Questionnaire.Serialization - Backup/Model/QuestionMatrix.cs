using Newtonsoft.Json;
using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("matrix")]
    public class QuestionMatrix : QuestionBaseSelect
    {
        public bool isAllRowRequired { get; set; } = false;
        public bool ShouldSerializeisAllRowRequired()
        {
            return this.isAllRowRequired;
        }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownColumn> columns { get; set; }        

        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }
    }
}
