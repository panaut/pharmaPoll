using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("MatrixRows")]
    public class MatrixRow : IValueTextPair, IVisitable
    {
        [JsonProperty(propertyName: "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return false;
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

        public int? ComplexMatrixId { get; set; }

        public bool ShouldSerializeComplexMatrixId()
        {
            return false;
        }

        public virtual ComplexMatrix ComplexMatrix { get; set; }

        public bool ShouldSerializeComplexMatrix()
        {
            return false;
        }

        public int? PlainMatrixId { get; set; }

        public bool ShouldSerializePlainMatrixId()
        {
            return false;
        }

        public virtual PlainMatrix PlainMatrix { get; set; }

        public bool ShouldSerializePlainMatrix()
        {
            return false;
        }

        [MaxLength(256)]
        [JsonProperty(PropertyName = "dbValue")]
        public string value { get; set; }

        public bool ShouldSerializevalue()
        {
            return false;
        }

        private string textProp;

        [MaxLength(256)]
        public string text
        {
            get
            {
                if (string.IsNullOrEmpty(textProp))
                {
                    return this.value;
                }
                else
                {
                    return textProp;
                }
            }
            set { textProp = value; }
        }

        [NotMapped]
        [JsonProperty(PropertyName = "value")]
        public string compositeValue
        {
            get
            {
                return $"{{Id:{this.Id}}}${value}";
            }
            set
            {
                var splitValues = value.Split(new[] { '$' }, 2);
                if (splitValues.Length > 1)
                {
                    this.Id = int.Parse(splitValues[0].Substring(4, splitValues[0].Length - 5));
                    this.value = splitValues[1];
                }
                else
                {
                    this.value = value;
                }

            }
        }

        public bool ShouldSerializecompositeValue()
        {
            return !string.IsNullOrEmpty(this.value);
        }

        public virtual void Visit(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
