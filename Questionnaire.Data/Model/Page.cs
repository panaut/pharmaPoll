using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    [Table("ElementContainers_Page")]
    public class Page : ElementContainer
    {
        [MaxLength(16)]
        public string navigationButtonsVisibility { get; set; } = "inherit";

        public bool ShouldSerializenavigationButtonsVisibility()
        {
            return this.navigationButtonsVisibility != "inherit";
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
