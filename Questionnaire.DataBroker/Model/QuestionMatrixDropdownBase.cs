using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionMatrixDropdownBase : QuestionBaseSelect
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
        public IEnumerable<ValueTextPair> rows { get; set; }

    }
}
