using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class Page
    {
        public string name { get; set; }

        public string title { get; set; }

        [JsonProperty(TypeNameHandling = TypeNameHandling.All)]
        public IEnumerable<QuestionBase> questions { get; set; } = new List<QuestionBase>();

        public int visibleIndex { get; set; } = -1;

        public string visibleIf { get; set; }

        public string navigationButtonsVisibility { get; set; } = "inherit";

        public Page()
        {

        }
    }
}
