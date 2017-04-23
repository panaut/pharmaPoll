using Newtonsoft.Json;
using Questionnaire.Serialization.Attributes;
using Questionnaire.Serialization.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("rating")]
    public class QuestionRating : QuestionBaseSelect
    {
        public string mininumRateDescription { get; set; } = null;
        public bool ShouldSerializemininumRateDescription()
        {
            return !string.IsNullOrEmpty(this.mininumRateDescription);
        }

        public string maximumRateDescription { get; set; } = null;
        public bool ShouldSerializemaximumRateDescription()
        {
            return !string.IsNullOrEmpty(this.maximumRateDescription);
        }

        [JsonProperty(PropertyName = "rateValues")]
        [JsonConverter(typeof(ValueTextPairConverter))]
        public override IEnumerable<Choice> choices { get; set; }

        public override bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }
    }
}
