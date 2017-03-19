﻿using Questionnaire.DataBroker.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("text")]
    public class QuestionText : QuestionBaseSelect
    {
        public int size { get; set; } = 25;
        public bool ShouldSerializesize()
        {
            return this.size != 25;
        }
        public string inputType { get; set; } = "text";
        public bool ShouldSerializeinputType()
        {
            return this.inputType != "text";
        }
        public string placeHolder { get; set; }
        public bool ShouldSerializeplaceHolder()
        {
            return !string.IsNullOrEmpty(this.placeHolder);
        }
    }
}
