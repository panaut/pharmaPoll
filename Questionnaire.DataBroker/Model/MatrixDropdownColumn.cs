using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class MatrixDropdownColumn : QuestionBase
    {
        public string optionsCaption { get; set; }

        public string minWidth { get; set; } = "";
        public string cellType { get; set; } = "default";
        public string inputType { get; set; } = "text";

        public string placeHolder { get; set; }
        public string choicesOrder { get; set; } = "none";
        public ChoicesRestfull choicesByUrl { get; set; }
        public int colCountValue { get; set; } = -1;
    }
}
