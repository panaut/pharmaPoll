using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("MatrixRow")]
    public class MatrixRow : IValueTextPair
    {
        [JsonPropertyAttribute(PropertyName = "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
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
        public string value { get; set; }

        [MaxLength(256)]
        public string text { get; set; }
    }
}
