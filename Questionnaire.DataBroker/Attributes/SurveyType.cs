using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Attributes
{
    public class SurveyType : Attribute
    {
        public string TypeIdentifier { get; set; }

        public SurveyType(string identifier)
        {
            this.TypeIdentifier = identifier;
        }

        public override bool Match(object obj)
        {
            return obj.ToString().ToLower().Equals(this.TypeIdentifier.ToLower());
        }
    }
}
