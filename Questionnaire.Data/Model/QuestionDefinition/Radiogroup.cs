using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("SelectBases_Radiogroup")]
    [SurveyType("radiogroup")]
    public class Radiogroup : SelectQuestionBase
    {
        public int colCount { get; set; }

        public bool ShouldSerializecolCount()
        {
            return this.colCount != 0;
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
