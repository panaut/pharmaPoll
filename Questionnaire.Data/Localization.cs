//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Questionnaire.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Localization
    {
        public int Id { get; set; }
        public string Culture { get; set; }
        public string LocalizedText { get; set; }
        public Nullable<int> QuestionTemplateId { get; set; }
        public Nullable<int> AnswerTemplateId { get; set; }
    
        public virtual QuestionTemplate QuestionTemplate { get; set; }
        public virtual AnswerTemplate AnswerTemplate { get; set; }
    }
}
