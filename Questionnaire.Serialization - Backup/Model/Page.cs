using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Questionnaire.Serialization.Model
{
    public class Page
    {
        public string name { get; set; }
        public bool ShouldSerializename()
        {
            return !string.IsNullOrEmpty(this.name);
        }

        public string title { get; set; }
        public bool ShouldSerializetitle()
        {
            return !string.IsNullOrEmpty(this.title);
        }

        [JsonProperty(ItemTypeNameHandling = TypeNameHandling.All)]
        public IEnumerable<Question> questions { get; set; } = new List<Question>();

        // proveriti ovo, treba da se overajduje IsNullOrEmpty
        public bool ShouldSerializequestions()
        {
            return this.questions != null && this.questions.Any();
        }

        public int visibleIndex { get; set; } = -1;
        public bool ShouldSerializevisibleIndex()
        {
            return this.visibleIndex != -1;
        }

        public string visibleIf { get; set; }
        public bool ShouldSerializevisibleIf()
        {
            return !string.IsNullOrEmpty(this.visibleIf);
        }

        public string navigationButtonsVisibility { get; set; } = "inherit";
        public bool ShouldSerializenavigationButtonsVisibility()
        {
            return this.navigationButtonsVisibility != "inherit";
        }

        public Page()
        {

        }
    }
}
