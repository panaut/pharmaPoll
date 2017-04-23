using Newtonsoft.Json;
using Questionnaire.Data.Model.QuestionDefinition;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model
{
    [Table("Pages")]
    public class Page
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

        public int innerIndent { get; set; }

        public bool ShouldSerializeinnerIndent()
        {
            return this.innerIndent != 0;
        }

        [MaxLength(256)]
        public string name { get; set; }

        public bool ShouldSerializename()
        {
            return !string.IsNullOrEmpty(this.name);
        }

        [MaxLength(16)]
        public string navigationButtonsVisibility { get; set; } = "inherit";

        public bool ShouldSerializenavigationButtonsVisibility()
        {
            return this.navigationButtonsVisibility != "inherit";
        }

        [MaxLength(256)]
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

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.All)]
        public virtual ICollection<QuestionBase> elements { get; set; } = new List<QuestionBase>();

        public bool ShouldSerializeelements()
        {
            return this.elements != null && this.elements.Any();
        }
    }
}
