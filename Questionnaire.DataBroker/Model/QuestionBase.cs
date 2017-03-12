using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class QuestionBase
    {
        string name { get; set; }

        public string title { get; set; }

        public QuestionCustomWidget customWidget { get; set; }

        public string visibleIf { get; set; }

        public bool startWithNewLine { get; set; } = true;

        public string width { get; set; }

        public int indent { get; set; }

        public bool isRequired { get; set; }

        public bool visible { get; set; }

        public IEnumerable<SurveyValidator> Validators { get; set; }

        public QuestionBase()
        {

        }
    }
}
