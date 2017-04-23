namespace Questionnaire.Serialization.Model
{
    public class ChoicesRestfull
    {

        public string Url { get; set; }

        public bool ShouldSerializeUrl()
        {
            return !string.IsNullOrEmpty(this.Url);
        }

        public string Path { get; set; }

        public bool ShouldSerializePath()
        {
            return !string.IsNullOrEmpty(this.Path);
        }
        public string ValueName { get; set; }

        public bool ShouldSerializeValueName()
        {
            return !string.IsNullOrEmpty(this.ValueName);
        }
        public string TitleName { get; set; }

        public bool ShouldSerializeTitleName()
        {
            return !string.IsNullOrEmpty(this.TitleName);
        }
    }
}
