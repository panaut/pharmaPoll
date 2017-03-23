using System;

namespace Questionnaire.Serialization.Attributes
{
    internal class SurveyType : Attribute
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
