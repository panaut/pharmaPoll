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
        public string columnMinWidth { get; set; } = "";

        public bool horizontalScroll { get; set; } = false;

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.None)]
        public IEnumerable<MatrixDropdownColumn> columns { get; set; }

        // ToDo: IC - Review this statement!
        // This property item type might just be 'MatrixDropDownRow'
        [JsonConverter(typeof(ValueTextPairConverter))]
        public IEnumerable<ValueTextPair> rows { get; set; }

    }
}
