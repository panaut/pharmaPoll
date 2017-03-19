﻿using Questionnaire.DataBroker.Attributes;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("checkbox")]
    public class QuestionCheckBox : QuestionBaseSelect
    {
        public int colCount { get; set; }
    }
}
