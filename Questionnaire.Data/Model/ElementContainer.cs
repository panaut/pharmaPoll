using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model
{
    [Table("ElementBases_ElementContainer")]
    public abstract class ElementContainer : ElementBase
    {
        public int innerIndent { get; set; }

        public bool ShouldSerializeinnerIndent()
        {
            return this.innerIndent != 0;
        }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.All)]
        public virtual ICollection<ElementBase> elements { get; set; } = new List<ElementBase>();

        public bool ShouldSerializeelements()
        {
            return this.elements != null && this.elements.Any();
        }
    }
}
