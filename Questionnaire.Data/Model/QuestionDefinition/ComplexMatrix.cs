using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("matrixdropdown")]
    [Table("ComplexMatrixBases_ComplexMatrix")]
    public class ComplexMatrix: ComplexMatrixBase
    {
        [JsonConverter(typeof(ValueTextPairConverter))]
        public virtual ICollection<MatrixRow> rows { get; set; } = new List<MatrixRow>();

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
