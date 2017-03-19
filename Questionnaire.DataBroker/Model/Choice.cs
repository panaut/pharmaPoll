using System;

namespace Questionnaire.DataBroker.Model
{
    public class Choice : IValueTextPair
    {
        public string text { get; set; }

        public string value { get; set; }
    }
}
