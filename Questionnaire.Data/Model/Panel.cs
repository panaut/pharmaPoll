using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model
{
    [SurveyType("panel")]
    [Table("ElementContainers_Panel")]
    public class Panel : ElementContainer
    {
    }
}
