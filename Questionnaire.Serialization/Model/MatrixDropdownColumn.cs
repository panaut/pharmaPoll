namespace Questionnaire.Serialization.Model
{
    public class MatrixDropdownColumn : QuestionBaseSelect, ICellDiversity, IValueTextPair
    {
        public string cellType { get; set; } = "default";

        public bool ShouldSerializecellType()
        {
            return this.cellType != "default";
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

        public int colCount { get; set; } = -1;

        public bool ShouldSerializecolCount()
        {
            return this.colCount != -1;
        }

        public string value { get; set; }

        public bool ShouldSerializevalue()
        {
            return !string.IsNullOrEmpty(this.value);
        }

        public string text { get; set; }

        public bool ShouldSerializetext()
        {
            return !string.IsNullOrEmpty(this.text);
        }
    }
}
