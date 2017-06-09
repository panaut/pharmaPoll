using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("ElementBases_QuestionBase")]
    public abstract class QuestionBase : ElementBase
    {
        public int? ComplexMatrixBaseId { get; set; }

        public bool ShouldSerializeComplexMatrixBaseId()
        {
            return false;
        }

        [ForeignKey("ComplexMatrixBaseId")]
        public virtual ComplexMatrixBase ComplexMatrixBase { get; set; }

        public bool ShouldSerializeComplexMatrixBase()
        {
            return false;
        }

        public bool startWithNewLine { get; set; } = true;

        public bool ShouldSerializestartWithNewLine()
        {
            return !this.startWithNewLine;
        }

        [MaxLength(16)]
        public string width { get; set; }

        public bool ShouldSerializewidth()
        {
            return !string.IsNullOrEmpty(this.width);
        }

        public int indent { get; set; }

        public bool ShouldSerializeindent()
        {
            return this.indent != 0;
        }

        public bool isRequired { get; set; } = false;

        public bool ShouldSerializeisRequired()
        {
            return this.isRequired;
        }

        [MaxLength(32)]
        public string renderAs { get; set; } = "standard";

        public bool ShouldSerializerenderAs()
        {
            return !string.IsNullOrEmpty(this.renderAs) && !this.renderAs.Equals("standard");
        }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Objects)]
        [JsonConverter(typeof(ValidatorsConverter))]
        public virtual ICollection<SurveyValidator> validators { get; set; } = new List<SurveyValidator>();

        public bool ShouldSerializevalidators()
        {
            return this.validators != null && this.validators.Any();
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }

        //public QuestionCustomWidget customWidget { get; set; }

        //public bool ShouldSerializecustomWidget()
        //{
        //    return this.customWidget != null;
        //}
    }
}
