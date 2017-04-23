using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("QDefBases")]
    public abstract class QuestionBase
    {
        [JsonPropertyAttribute(PropertyName = "internalId")]
        public int Id { get; set; }

        public bool ShouldSerializeId()
        {
            return this.Id != 0;
        }

        public int PageId { get; set; }

        public bool ShouldSerializePageId()
        {
            return false;
        }

        public virtual Page Page { get; set; }

        public bool ShouldSerializePage()
        {
            return false;
        }

        // ToDo: IC - Implement this relation 'backward'
        //public int SurveyId { get; set; }

        //public bool ShouldSerializeSurveyId()
        //{
        //    return false;
        //}

        //public virtual Survey Survey { get; set; }

        //public bool ShouldSerializeSurvey()
        //{
        //    return false;
        //}

        [MaxLength(512)]
        public string name { get; set; }

        [MaxLength(512)]
        public string title { get; set; }

        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        [MaxLength(1024)]
        public string visibleIf { get; set; }

        public bool ShouldSerializevisibleIf()
        {
            return !string.IsNullOrEmpty(this.visibleIf);
        }

        public bool readOnly { get; set; }

        public bool ShouldSerializereadOnly()
        {
            return !this.readOnly;
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

        public bool visible { get; set; } = true;

        public bool ShouldSerializevisible()
        {
            return !this.visible;
        }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.Objects)]
        [JsonConverter(typeof(ValidatorsConverter))]
        public virtual ICollection<SurveyValidator> validators { get; set; } = new List<SurveyValidator>();

        public bool ShouldSerializevalidators()
        {
            return this.validators != null && this.validators.Any();
        }

        //public QuestionCustomWidget customWidget { get; set; }

        //public bool ShouldSerializecustomWidget()
        //{
        //    return this.customWidget != null;
        //}
    }
}
