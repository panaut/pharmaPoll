﻿using Questionnaire.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Serialization.Model
{
    [SurveyType("radiogroup")]
    public class QuestionRadiogroup : QuestionBaseSelect
    {
        public int colCount { get; set; }
        public bool ShouldSerializecolCount()
        {
            return this.colCount != 0;
        }
    }
}
