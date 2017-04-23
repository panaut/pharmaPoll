using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    // ToDo: IC - Implement All Triggers
    [Table("Triggers")]
    public class SurveyTrigger
    {
        [JsonPropertyAttribute(PropertyName = "internalId")]
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
    }
}
