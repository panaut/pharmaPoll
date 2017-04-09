using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Serialization.Model
{
    public class Question
    {
        public string name { get; set; }

        public string title { get; set; }

        public int internalId { get; set; }

        public bool ShouldSerializeinternalId()
        {
            return this.internalId != 0;
        }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        public string commentText { get; set; }

        public bool ShouldSerializecommentText()
        {
            return !string.IsNullOrEmpty(this.commentText);
        }

        public QuestionCustomWidget customWidget { get; set; }

        public bool ShouldSerializecustomWidget()
        {
            return this.customWidget != null;
        }

        public string visibleIf { get; set; }

        public bool ShouldSerializevisibleIf()
        {
            return !string.IsNullOrEmpty(this.visibleIf);
        }

        public bool startWithNewLine { get; set; } = true;

        public bool ShouldSerializestartWithNewLine()
        {
            return !this.startWithNewLine;
        }

        public string width { get; set; }

        public bool ShouldSerializewidth()
        {
            return !string.IsNullOrEmpty(this.width);
        }

        public int indent { get; set; }

        public bool ShouldSerializeindent()
        {            
            return this.indent != 0;
        }

        public bool isRequired { get; set; } = false;

        public bool ShouldSerializeisRequired()
        {
            return this.isRequired;
        }

        public bool visible { get; set; } = true;

        public bool ShouldSerializevisible()
        {
            return !this.visible;
        }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Objects)]
        [JsonConverter(typeof(ValidatorsConverter))]
        public IEnumerable<SurveyValidator> validators { get; set; }

        public bool ShouldSerializevalidators()
        {
            return this.validators != null && this.validators.Any();
        }
    }
}
