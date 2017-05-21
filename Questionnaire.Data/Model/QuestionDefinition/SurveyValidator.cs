using Newtonsoft.Json;
using Questionnaire.Data.Attributes;
using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("Validators")]
    public abstract class SurveyValidator
    {
        [JsonProperty(PropertyName = "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public int QuestionId { get; set; }

        public bool ShouldSerializeQuestionId()
        {
            return false;
        }

        [ForeignKey("QuestionId")]
        public virtual QuestionBase Question { get; set; }

        public bool ShouldSerializeQuestion()
        {
            return false;
        }

        [LocalizableProperty()]
        [MaxLength(256)]
        public string text { get; set; }

        public bool ShouldSerializeText()
        {
            return !string.IsNullOrEmpty(this.text);
        }
    }

    [SurveyType("numeric")]
    [Table("Validators_Numeric")]
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
    [Table("Validators_Text")]
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
    [Table("Validators_AnswerCount")]
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

    [SurveyType("email")]
    [Table("Validators_Email")]
    public class EmailValidator : SurveyValidator
    {

    }

    [SurveyType("regex")]
    [Table("Validators_Regex")]
    public class RegexValidator : SurveyValidator
    {
        [MaxLength(1024)]
        public string regex { get; set; }

        public bool ShouldSerializeregex()
        {
            return !string.IsNullOrEmpty(this.regex);
        }
    }
}
