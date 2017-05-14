using Newtonsoft.Json;
using Questionnaire.Data.Attributes;
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
        [LocalizableProperty()]
        public string minRateDescription { get; set; } = null;

        public bool ShouldSerializemininumRateDescription()
        {
            return !string.IsNullOrEmpty(this.minRateDescription);
        }

        [MaxLength(256)]
        [LocalizableProperty()]
        public string maxRateDescription { get; set; } = null;

        public bool ShouldSerializemaximumRateDescription()
        {
            return !string.IsNullOrEmpty(this.maxRateDescription);
        }

        [JsonProperty(PropertyName = "rateValues")]
        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<Choice> choices { get; set; } = new List<Choice>();

        public bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
