using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("multipletext")]
    public class QuestionMultipleText : Question
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

        public IEnumerable<MultipleTextQuestionItem> items { get; set; }
    }
}
