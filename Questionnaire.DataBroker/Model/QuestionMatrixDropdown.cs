using Newtonsoft.Json;
using Questionnaire.DataBroker.Attributes;
using System.Collections.Generic;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("matrixdropdown")]
    public class QuestionMatrixDropdown : QuestionBaseSelect
    {
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

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.None)]
        public IEnumerable<MatrixDropdownColumn> columns { get; set; }

        // ToDo: IC - Review this statement!
        // This property item type might just be 'MatrixDropDownRow'
        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<MatrixDropdownRow> rows { get; set; }

    }
}
