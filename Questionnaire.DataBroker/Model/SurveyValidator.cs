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
        public bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(this.Text);
        }
    }

    [SurveyType("numeric")]
    public class NumericValidator : SurveyValidator
    {
        public int? minValue { get; set; }
        public bool ShouldSerializeminValue()
        {
            return this.minValue.HasValue;
        }
                
        public int? maxValue { get; set; }
        public bool ShouldSerializemaxValue()
        {
            return this.maxValue.HasValue;
        }
    }

    [SurveyType("text")]
    public class TextValidator : SurveyValidator
    {
        public int? minLength { get; set; }
        public bool ShouldSerializeminLength()
        {
            return this.minLength.HasValue;
        }

        public int? maxLength { get; set; }
        public bool ShouldSerializemaxLength()
        {
            return this.maxLength.HasValue;
        }
    }

    [SurveyType("answercount")]
    public class AnswerCountValidator : SurveyValidator
    {
        public int? minCount { get; set; }
        public bool ShouldSerializeminCount()
        {
            return this.minCount.HasValue;
        }

        public int? maxCount { get; set; }
        public bool ShouldSerializemaxCount()
        {
            return this.maxCount.HasValue;
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
