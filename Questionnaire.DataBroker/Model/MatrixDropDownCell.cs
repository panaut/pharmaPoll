using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class MatrixDropDownCell
    {
        public Question questionValue { get; set; }
        
        
        public bool ShouldSerializequestionValue()
        {
            return this.questionValue != null;
        }
    }
}
