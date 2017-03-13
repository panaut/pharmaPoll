using Questionnaire.DataBroker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker
{
    public class QuestionCommentBase : QuestionBaseSelect
    {
        public int rows { get; set; } = 4;
        public int cols { get; set; } = 50;
        public string placeHolder { get; set; }
    }
}
