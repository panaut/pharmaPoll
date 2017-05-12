using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    [Table("ElementBases")]
    public abstract class ElementBase : IVisitable
    {
        [JsonPropertyAttribute(PropertyName = "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public int SurveyId { get; set; }

        public bool ShouldSerializeSurveyId()
        {
            return false;
        }

        public virtual Survey Survey { get; set; }

        public bool ShouldSerializeSurvey()
        {
            return false;
        }

        public int PositionWithinContainer { get; set; }

        public bool ShouldSerializePositionWithinContainer()
        {
            return false;
        }

        public int? ElementContainerId { get; set; }

        public bool ShouldSerializeElementContainerId()
        {
            return false;
        }

        [NotMapped]
        public int? ElementContainerUId { get; set; }

        public bool ShouldSerializeElementContainerUId()
        {
            return false;
        }

        public virtual ElementContainer ElementContainer { get; set; }

        public bool ShouldSerializeElementContainer()
        {
            return false;
        }

        [MaxLength(512)]
        public string name { get; set; }

        public bool ShouldSerializename()
        {
            return !string.IsNullOrEmpty(this.name);
        }

        [MaxLength(512)]
        public string title { get; set; }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        public bool visible { get; set; } = true;

        public bool ShouldSerializevisible()
        {
            return !this.visible;
        }

        [MaxLength(1024)]
        public string visibleIf { get; set; }

        public bool ShouldSerializevisibleIf()
        {
            return !string.IsNullOrEmpty(this.visibleIf);
        }

        public bool readOnly { get; set; }

        public bool ShouldSerializereadOnly()
        {
            return this.readOnly;
        }

        public virtual void Visit(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
