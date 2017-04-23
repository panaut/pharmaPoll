﻿using Newtonsoft.Json;
using Questionnaire.Serialization.Model;
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