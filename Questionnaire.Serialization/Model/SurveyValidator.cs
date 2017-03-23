using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    public abstract class SurveyValidator
    {
        public string Text { get; set; }
        public bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(this.Text);
        }
    }

    [SurveyType("numeric")]
    public class NumericValidator : SurveyValidator
    {
        public int minValue { get; set; }
        public bool ShouldSerializeminValue()
        {
            return this.minValue != 0;
        }
                
        public int maxValue { get; set; }
        public bool ShouldSerializemaxValue()
        {
            return this.maxValue != 0;
        }
    }

    [SurveyType("text")]
    public class TextValidator : SurveyValidator
    {
        public int minLength { get; set; }
        public bool ShouldSerializeminLength()
        {
            return this.minLength != 0;
        }

        public int maxLength { get; set; }
        public bool ShouldSerializemaxLength()
        {
            return this.maxLength != 0;
        }
    }

    [SurveyType("answercount")]
    public class AnswerCountValidator : SurveyValidator
    {
        public int minCount { get; set; }
        public bool ShouldSerializeminCount()
        {            
            return this.minCount != 0;
        }

        public int maxCount { get; set; }
        public bool ShouldSerializemaxCount()
        {
            return this.maxCount != 0;
        }
    }

    [SurveyType("regex")]
    public class RegexValidator : SurveyValidator
    {
        public string regex { get; set; }
        public bool ShouldSerializeregex()
        {
            return !string.IsNullOrEmpty(this.regex);
        }
    }

    [SurveyType("email")]
    public class EmailValidator : SurveyValidator
    {

    }
}
