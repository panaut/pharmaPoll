﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.DataBroker.Model
{
    public class MatrixDropdownRow : Question, IValueTextPair
    {
        public string value { get; set; }

        public string text { get; set; }

        public MatrixDropDownCell cells { get; set; }
    }
}
