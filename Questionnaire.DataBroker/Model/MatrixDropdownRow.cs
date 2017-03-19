using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class MatrixDropdownRow : Question, IValueTextPair
    {
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

        public MatrixDropDownCell cells { get; set; }
        
        // proveriti ovo, treba da se overajduje IsNullOrEmpty
        public bool ShouldSerializecells()
        {
            return this.cells != null;
        }
    }
}
