using System;

namespace Questionnaire.DataBroker.Model
{
    public class Choice : IValueTextPair
    {
        public string text { get; set; }
        public bool ShouldSerializetext()
        {
            return !string.IsNullOrEmpty(this.text);
        }

        public string value { get; set; }
        public bool ShouldSerializevalue()
        {
            return !string.IsNullOrEmpty(this.value);
        }
    }
}
