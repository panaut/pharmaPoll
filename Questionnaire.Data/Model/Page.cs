using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Questionnaire.Data.Model
{
    [Table("ElementContainers_Page")]
    public class Page : ElementContainer
    {
        //public int ParentSurveyId { get; set; }

        //public bool ShouldSerializeParentSurveyId()
        //{
        //    return false;
        //}

        //public virtual Survey ParentSurvey { get; set; }

        //public bool ShouldSerializeParentSurvey()
        //{
        //    return false;
        //}

        [MaxLength(16)]
        public string navigationButtonsVisibility { get; set; } = "inherit";

        public bool ShouldSerializenavigationButtonsVisibility()
        {
            return this.navigationButtonsVisibility != "inherit";
        }
    }
}
