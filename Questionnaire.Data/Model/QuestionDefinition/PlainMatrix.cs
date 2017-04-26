using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("matrix")]
    [Table("QuestionBases_PlainMatrix")]
    public class PlainMatrix : QuestionBase
    {
        public bool isAllRowRequired { get; set; } = false;
        public bool ShouldSerializeisAllRowRequired()
        {
            return this.isAllRowRequired;
        }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<Choice> columns { get; set; } = new List<Choice>();

        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<MatrixRow> rows { get; set; } = new List<MatrixRow>();
    }
}
