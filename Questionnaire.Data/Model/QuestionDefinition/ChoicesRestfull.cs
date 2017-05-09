using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("RestfullChoiceSources")]
    public class RestfullChoiceSource : IVisitable
    {
        [ForeignKey("Question")]
        [JsonProperty(propertyName: "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public virtual SelectQuestionBase Question { get; set; }

        public bool ShouldSerializeQuestion()
        {
            return false;
        }

        public string Url { get; set; }

        public bool ShouldSerializeUrl()
        {
            return !string.IsNullOrEmpty(this.Url);
        }

        public string Path { get; set; }

        public bool ShouldSerializePath()
        {
            return !string.IsNullOrEmpty(this.Path);
        }
        public string ValueName { get; set; }

        public bool ShouldSerializeValueName()
        {
            return !string.IsNullOrEmpty(this.ValueName);
        }
        public string TitleName { get; set; }

        public bool ShouldSerializeTitleName()
        {
            return !string.IsNullOrEmpty(this.TitleName);
        }

        public virtual void Visit(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
