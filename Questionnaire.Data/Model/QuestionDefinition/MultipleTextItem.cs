﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [Table("QuestionBases_MultipleTextItem")]
    public class MultipleTextItem : QuestionBase
    {
        public int? MultipleTextQuestionId { get; set; }

        public bool ShouldSerializeMultipleTextQuestionId()
        {
            return false;
        }

        [ForeignKey("MultipleTextQuestionId")]
        public MultipleText MultipleTextQuestion { get; set; }

        public bool ShouldSerializeMultipleTextQuestion()
        {
            return false;
        }

        public override void Visit(IVisitor visitor)
        {
            base.Visit(visitor);
            visitor.Visit(this);
        }
    }
}
