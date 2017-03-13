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
        public IEnumerable<MatrixDropdownColumn> columns { get; set; }
        public IEnumerable<MatrixDropdownRow> rows { get; set; }

    }
}
