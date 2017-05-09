using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("matrixdynamic")]
    [Table("ComplexMatrixBases_ComplexMatrixDynamic")]
    public class ComplexMatrixDynamic : ComplexMatrixBase
    {
        [MaxLength(64)]
        public string addRowText { get; set; }

        public bool ShouldSerializeaddRowText()
        {
            return !string.IsNullOrEmpty(this.addRowText);
        }

        [MaxLength(64)]
        public string removeRowText { get; set; }

        public bool ShouldSerializeremoveRowText()
        {
            return !string.IsNullOrEmpty(this.addRowText);
        }

        public int rowCount { get; set; }

        public bool ShouldSerializerowCount()
        {
            return this.rowCount != 0;
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
