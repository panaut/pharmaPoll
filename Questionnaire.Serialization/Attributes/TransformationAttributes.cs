using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// ToDo: IC - Complete the implementation of localization attributes
namespace Questionnaire.Serialization.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LocalizationTargetType : Attribute
    {
        public Type Type { get; set; }

        public LocalizationTargetType(Type type)
        {
            this.Type = type;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizableString : Attribute
    {
        public string TargetProperty { get; set; }

        public LocalizableString()
        {

        }

        public LocalizableString(string targetProperty)
        {
            this.TargetProperty = targetProperty;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizableProperty : Attribute
    {
        public string TargetProperty { get; set; }

        public LocalizableProperty()
        {

        }

        public LocalizableProperty(string targetProperty)
        {
            this.TargetProperty = targetProperty;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LocalizableCollectionProperty : Attribute
    {
        public string TargetProperty { get; set; }

        public LocalizableCollectionProperty()
        {

        }

        public LocalizableCollectionProperty(string targetProperty)
        {
            this.TargetProperty = targetProperty;
        }
    }
}
