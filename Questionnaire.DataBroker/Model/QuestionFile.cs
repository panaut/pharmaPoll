﻿using Questionnaire.DataBroker.Attributes;

namespace Questionnaire.DataBroker.Model
{
    [SurveyType("file")]
    public class QuestionFile : QuestionBaseSelect
    {
        public string imageHeight { get; set; }
        public bool ShouldSerializeimageHeight()
        {
            return !string.IsNullOrEmpty(this.imageHeight);
        }

        public string imageWidth { get; set; }
        public bool ShouldSerializeimageWidth()
        {
            return !string.IsNullOrEmpty(this.imageWidth);
        }

        public bool? storeDataAsText { get; set; }
        public bool ShouldSerializestoreDataAsText()
        {
            return this.storeDataAsText.HasValue;
        }

        public int? maxSize { get; set; }
        public bool ShouldSerializemaxSize()
        {
            return this.maxSize.HasValue;
        }
    }
}
