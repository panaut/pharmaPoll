using Newtonsoft.Json;
using Questionnaire.Data.Attributes;
using Questionnaire.Data.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("QuestionBases_SelectBases")]
    public abstract class SelectQuestionBase : QuestionBase
    {
        public bool hasComment { get; set; } = false;

        public bool ShouldSerializehasComment()
        {
            return this.hasComment;
        }

        [MaxLength(1024)]
        [LocalizableProperty()]
        public string commentText { get; set; }

        public bool ShouldSerializecommentText()
        {
            return !string.IsNullOrEmpty(this.commentText);
        }

        [MaxLength(8)]
        public string choicesOrder { get; set; }

        public bool ShouldSerializechoicesOrder()
        {
            return !string.IsNullOrEmpty(this.choicesOrder);
        }

        public virtual RestfullChoiceSource choicesByUrl { get; set; }

        public bool ShouldSerializechoicesByUrl()
        {
            return this.choicesByUrl != null;
        }

        public bool hasOther { get; set; }

        public bool ShouldSerializehasOther()
        {
            return this.hasOther != false;
        }

        [MaxLength(256)]
        [LocalizableProperty()]
        public string otherText { get; set; }

        public bool ShouldSerializeotherText()
        {
            return !string.IsNullOrEmpty(this.otherText);
        }

        [MaxLength(1024)]
        [LocalizableProperty()]
        public string otherErrorText { get; set; }

        public bool ShouldSerializeotherErrorText()
        {
            return !string.IsNullOrEmpty(this.otherErrorText);
        }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<Choice> choices { get; set; } = new List<Choice>();

        public bool ShouldSerializechoices()
        {
            return this.choices != null && this.choices.Any();
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
