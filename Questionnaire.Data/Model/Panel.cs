using Newtonsoft.Json;
using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model
{
    [SurveyType("panel")]
    [Table("ElementContainers_Panel")]
    public class Panel : ElementContainer
    {
    }
}
