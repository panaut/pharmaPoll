using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Serialization.Model
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

        public bool hasOther { get; set; }

        public bool ShouldSerializehasOther()
        {
            return this.hasOther != false;
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
        public virtual IEnumerable<Choice> choices { get; set; }

        public virtual bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }
    }
}
