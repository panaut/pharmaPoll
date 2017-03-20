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
        public bool ShouldSerializeisAllRowRequired()
        {
            return this.isAllRowRequired;
        }


        public IEnumerable<MatrixDropdownColumn> columns { get; set; }
        public bool ShouldSerializecolumns()
        {
            return this.columns != null && this.columns.Any();
        }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }
    }
}
