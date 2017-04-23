using Questionnaire.Serialization.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("SelectBases_ComplexMatrixBase")]
    public abstract class ComplexMatrixBase : SelectQuestionBase, ICellDiversity
    {
        [MaxLength(16)]
        public string cellType { get; set; } = "dropdown";

        public bool ShouldSerializecellType()
        {
            return !this.cellType.Equals("dropdown");
        }

        public int columnColCount { get; set; }

        public bool ShouldSerializecolumnColCount()
        {
            return this.columnColCount != 0;
        }

        [MaxLength(16)]
        public string columnMinWidth { get; set; }

        public bool ShouldSerializecolumnMinWidth()
        {
            return !string.IsNullOrEmpty(this.columnMinWidth);
        }

        public bool horizontalScroll { get; set; } = false;

        public bool ShouldSerializehorizontalScroll()
        {
            return this.horizontalScroll;
        }

        public virtual ICollection<MatrixColumn> columns { get; set; } = new List<MatrixColumn>();
    }
}
