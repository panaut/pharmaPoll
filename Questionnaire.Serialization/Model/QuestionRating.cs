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

        public IEnumerable<IValueTextPair> RateValues { get; set; }
        public bool ShouldSerializeRateValues()
        {
            return this.RateValues != null && this.RateValues.Any();
        }
    }
}
