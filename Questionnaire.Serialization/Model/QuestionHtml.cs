using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("html")]
    public class QuestionHtml : QuestionBaseSelect
    {
        public string html { get; set; }
        public bool ShouldSerializehtml()
        {
            return !string.IsNullOrEmpty(this.html);
        }
    }
}
