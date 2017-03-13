using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionMatrixBase : QuestionBaseSelect
    {
        public bool isAllRowRequired { get; set; } = false;
        public IEnumerable<ValueText> columns { get; set; }
        public IEnumerable<ValueText> rows { get; set; }
    }
}
