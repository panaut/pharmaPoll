using Questionnaire.DataBroker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public abstract class SurveyValidator
    {
        public string Text { get; set; }
    }

    [SurveyType("numeric")]
    public class NumericValidator : SurveyValidator
    {
        public int minValue { get; set; }

        public int maxValue { get; set; }
    }

    [SurveyType("text")]
    public class TextValidator : SurveyValidator
    {
        public int minLength { get; set; }

        public int maxLength { get; set; }
    }

    [SurveyType("answercount")]
    public class AnswerCountValidator : SurveyValidator
    {
        public int minCount { get; set; }

        public int maxCount { get; set; }
    }

    [SurveyType("regex")]
    public class RegexValidator : SurveyValidator
    {
        public string regex { get; set; }
    }

    [SurveyType("email")]
    public class EmailValidator : SurveyValidator
    {

    }
}
