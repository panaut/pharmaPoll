using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    public class LocalizedString
    {
        [Key]
        [Column(Order = 1)]
        public string TypeIdentifier { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TypeUniqueId { get; set; }

        [Key]
        [Column(Order = 3)]
        public string FieldIdentifier { get; set; }

        [Key]
        [Column(Order = 4)]
        public ECulture Culture { get; set; }

        public int SurveyId { get; set; }

        public Survey Survey { get; set; }

        [MaxLength(1024)]
        public string LocalizedValue { get; set; }
    }
}
