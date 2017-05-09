using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("dropdown")]
    [Table("SelectBases_Dropdown")]
    public class Dropdown : SelectQuestionBase
    {
        [MaxLength(256)]
        public string optionsCaption { get; set; }

        public bool ShouldSerializeoptionsCaption()
        {
            return !string.IsNullOrEmpty(this.optionsCaption);
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
