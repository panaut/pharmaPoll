using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("QuestionBases_Rating")]
    [SurveyType("rating")]
    public class Rating : QuestionBase
    {
        [MaxLength(256)]
        public string mininumRateDescription { get; set; } = null;

        public bool ShouldSerializemininumRateDescription()
        {
            return !string.IsNullOrEmpty(this.mininumRateDescription);
        }

        [MaxLength(256)]
        public string maximumRateDescription { get; set; } = null;

        public bool ShouldSerializemaximumRateDescription()
        {
            return !string.IsNullOrEmpty(this.maximumRateDescription);
        }

        [JsonProperty(PropertyName = "rateValues")]
        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<Choice> choices { get; set; }

        public bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }
    }
}
