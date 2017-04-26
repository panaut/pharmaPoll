using Questionnaire.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("multipletext")]
    [Table("QuestionBases_MultipleText")]
    public class MultipleText : QuestionBase
    {
        public int colCount { get; set; }

        public bool ShouldSerializecolCount()
        {
            return this.colCount != 0;
        }

        public int itemSize { get; set; }

        public bool ShouldSerializeitemSize()
        {
            return this.itemSize != 25;
        }

        public virtual ICollection<MultipleTextItem> items { get; set; } = new List<MultipleTextItem>();
    }
}
