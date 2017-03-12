using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionBaseSelect : QuestionBase
    {

        public bool hasComment { get; set; }

        public string choicesOrder { get; set; }

        public ChoicesRestfull choicesByUrl { get; set; }

        public string commentText { get; set; }

        public bool hasOther { get; set; }

        public string otherText { get; set; }

        public string otherErrorText { get; set; }

        public IEnumerable<Choice> choices { get; set; }

        public QuestionBaseSelect()
        {

        }
    }
}
