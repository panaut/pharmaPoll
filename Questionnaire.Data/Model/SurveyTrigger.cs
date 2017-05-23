using Newtonsoft.Json;
using Questionnaire.Data.Serialization;
using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    [Table("Triggers")]
    public abstract class SurveyTrigger
    {
        [JsonProperty(PropertyName = "internalId")]
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

        [MaxLength(16)]
        [JsonProperty(PropertyName = "operator")]
        public string relation { get; set; }

        [MaxLength(512)]
        public string value { get; set; }

        [MaxLength(512)]
        public string name { get; set; }
    }

    [SurveyType("visible")]
    [Table("Triggers_Visibility")]
    public class VisibilityTrigger : SurveyTrigger
    {
        [JsonConverter(typeof(StringObjectsConverter))]
        public string pages { get; set; }

        [JsonConverter(typeof(StringObjectsConverter))]
        public string questions { get; set; }
    }

    [SurveyType("complete")]
    [Table("Triggers_Complete")]
    public class CompleteTrigger : SurveyTrigger
    {

    }

    [SurveyType("setvalue")]
    [Table("Triggers_SetValue")]
    public class SetValueTrigger : SurveyTrigger
    {
        [MaxLength(512)]
        public string setToName { get; set; }

        [MaxLength(512)]
        public string setValue { get; set; }

        public bool isVariable { get; set; }

        public bool ShouldSerializeisVariable()
        {
            return this.isVariable;
        }
    }
}
