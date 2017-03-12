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

    public class NumericValidator : SurveyValidator
    {
        public int minValue { get; set; }

        public int maxValue { get; set; }

        public NumericValidator()
        {

        }
    }

    public class TextValidator : SurveyValidator
    {
        public int minLenght { get; set; }

        public int maxLenght { get; set; }

        public TextValidator()
        {

        }
    }

    public class AnswerCountValidator : SurveyValidator
    {
        public int minCount { get; set; }

        public int maxCount { get; set; }

        public AnswerCountValidator()
        {

        }
    }

    public class RegexValidator : SurveyValidator
    {
        public string regex { get; set; }
    }

    public class EmailValidator : SurveyValidator
    {

    }
}
