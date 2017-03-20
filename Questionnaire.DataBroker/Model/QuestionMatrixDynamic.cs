using Questionnaire.DataBroker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("matrixdynamic")]
    public class QuestionMatrixDynamic : QuestionMatrixDropdownBase
    {
        public string addRowText { get; set; }

        public bool ShouldSerializeaddRowText()
        {
            return !string.IsNullOrEmpty(this.addRowText);
        }

        public string removeRowText { get; set; }

        public bool ShouldSerializeremoveRowText()
        {
            return !string.IsNullOrEmpty(this.addRowText);
        }

        public int rowCount { get; set; }

        public bool ShouldSerializerowCount()
        {
            return this.rowCount != 0;
        }
    }
}
