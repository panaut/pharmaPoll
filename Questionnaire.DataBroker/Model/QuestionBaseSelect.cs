using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionBaseSelect : Question
    {

        public bool hasComment { get; set; } = false;

        public bool ShouldSerializehasComment()
        {
            return this.hasComment;
        }

        public string choicesOrder { get; set; }

        public bool ShouldSerializechoicesOrder()
        {
            return !string.IsNullOrEmpty(this.choicesOrder);
        }

        public ChoicesRestfull choicesByUrl { get; set; }

        public bool ShouldSerializechoicesByUrl()
        {
            return this.choicesByUrl != null;
        }

        public string commentText { get; set; }

        public bool ShouldSerializecommentText()
        {
            return !string.IsNullOrEmpty(this.commentText);
        }
        
        public bool hasOther { get; set; }

        public bool ShouldSerializehasOther()
        {
            return this.hasOther;
        }

        public string otherText { get; set; }

        public bool ShouldSerializeotherText()
        {
            return !string.IsNullOrEmpty(this.otherText);
        }

        public string otherErrorText { get; set; }

        public bool ShouldSerializeotherErrorText()
        {
            return !string.IsNullOrEmpty(this.otherErrorText);
        }


        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<Choice> choices { get; set; }

        public bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }
    }
}
