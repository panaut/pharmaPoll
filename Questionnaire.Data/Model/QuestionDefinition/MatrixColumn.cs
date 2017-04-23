using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("MatrixColumn")]
    public class MatrixColumn : SelectQuestionBase, ICellDiversity
    {
        public int? ComplexMatrixBaseId { get; set; }

        public bool ShouldSerializeComplexMatrixBaseId()
        {
            return false;
        }

        public virtual ComplexMatrixBase ComplexMatrixBase { get; set; }

        public bool ShouldSerializeComplexMatrixBase()
        {
            return false;
        }

        public int? PlainMatrixId { get; set; }

        public bool ShouldSerializePlainMatrixId()
        {
            return false;
        }

        public virtual PlainMatrix PlainMatrix { get; set; }

        public bool ShouldSerializePlainMatrix()
        {
            return false;
        }

        [MaxLength(32)]
        public string cellType { get; set; } = "default";

        public bool ShouldSerializecellType()
        {
            return this.cellType != "default";
        }

        [MaxLength(32)]
        public string inputType { get; set; } = "text";

        public bool ShouldSerializeinputType()
        {
            return this.inputType != "text";
        }

        [MaxLength(256)]
        public string placeHolder { get; set; }

        public bool ShouldSerializeplaceHolder()
        {
            return !string.IsNullOrEmpty(this.placeHolder);
        }

        public int colCount { get; set; } = -1;

        public bool ShouldSerializecolCount()
        {
            return this.colCount != -1;
        }
    }
}
