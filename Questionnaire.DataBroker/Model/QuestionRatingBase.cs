using Questionnaire.DataBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionRatingBase : QuestionBaseSelect
    {
        public string mininumRateDescription { get; set; } = null;
        public string maximumRateDescription { get; set; } = null;
        public IEnumerable<ValueText> RateValues { get; set; }
    }
}
