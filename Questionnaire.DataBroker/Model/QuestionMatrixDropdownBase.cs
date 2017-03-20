using Newtonsoft.Json;
using System.Collections.Generic;

namespace Questionnaire.DataBroker.Model
{
    public abstract class QuestionMatrixDropdownBase : QuestionBaseSelect, ICellDiversity
    {
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

        public IEnumerable<MatrixDropdownColumn> columns { get; set; }

        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }
    }
}
