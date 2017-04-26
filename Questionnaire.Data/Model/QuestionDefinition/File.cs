using Questionnaire.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Questionnaire.Data.Model.QuestionDefinition
{
    [SurveyType("file")]
    [Table("QuestionBases_File")]
    public class File : QuestionBase
    {
        [MaxLength(16)]
        public string imageHeight { get; set; }

        public bool ShouldSerializeimageHeight()
        {
            return !string.IsNullOrEmpty(this.imageHeight);
        }

        [MaxLength(16)]
        public string imageWidth { get; set; }

        public bool ShouldSerializeimageWidth()
        {
            return !string.IsNullOrEmpty(this.imageWidth);
        }

        public bool showPreview { get; set; }

        public bool ShouldSerializeshowPreview()
        {
            return this.showPreview != false;
        }

        public bool storeDataAsText { get; set; }

        public bool ShouldSerializestoreDataAsText()
        {
            return this.storeDataAsText != false;
        }

        public int maxSize { get; set; }

        public bool ShouldSerializemaxSize()
        {
            return this.maxSize != 0;
        }
    }
}
