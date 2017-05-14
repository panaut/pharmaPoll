using System;

namespace Questionnaire.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizableProperty : Attribute
    {
        public string FieldIdentifier { get; set; }

        public LocalizableProperty()
        {

        }

        public LocalizableProperty(string fieldIdentifier)
        {
            this.FieldIdentifier = fieldIdentifier;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class LocalizationTypeIdentifier : Attribute
    {
        public string TypeIdentifier { get; set; }

        public LocalizationTypeIdentifier()
        {

        }

        public LocalizationTypeIdentifier(string typeIdentifier)
        {
            this.TypeIdentifier = typeIdentifier;
        }
    }
}
