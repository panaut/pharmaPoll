using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionTextBase : QuestionBaseSelect
    {
        public int size { get; set; } = 25;
        public string inputType { get; set; } = "text";
        public string placeHolder { get; set; }        
    }
}
