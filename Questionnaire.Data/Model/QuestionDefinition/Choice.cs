using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("Choices")]
    public class Choice : IValueTextPair
    {
        [JsonProperty(propertyName: "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public int SurveyId { get; set; }

        public bool ShouldSerializeSurveyId()
        {
            return false;
        }

        public virtual Survey Survey { get; set; }

        public bool ShouldSerializeSurvey()
        {
            return false;
        }

        public int? QuestionId { get; set; }

        public bool ShouldSerializeQuestionId()
        {
            return false;
        }

        public int? RatingId { get; set; }

        public bool ShouldSerializeRatingId()
        {
            return false;
        }

        public int? PlainMatrixId { get; set; }

        public bool ShouldSerializePlainMatrixId()
        {
            return false;
        }

        [ForeignKey("QuestionId")]
        public virtual SelectQuestionBase Question { get; set; }

        public bool ShouldSerializeQuestion()
        {
            return false;
        }

        [ForeignKey("RatingId")]
        public virtual Rating Rating { get; set; }

        public bool ShouldSerializeRating()
        {
            return false;
        }

        [ForeignKey("PlainMatrixId")]
        public virtual PlainMatrix PlainMatrix { get; set; }

        public bool ShouldSerializePlainMatrix()
        {
            return false;
        }

        [MaxLength(256)]
        public string text { get; set; }

        public bool ShouldSerializetext()
        {
            return !string.IsNullOrEmpty(this.text);
        }

        [MaxLength(256)]
        public string value { get; set; }

        public bool ShouldSerializevalue()
        {
            return !string.IsNullOrEmpty(this.value);
        }
    }
}
