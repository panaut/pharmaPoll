using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Infrastructure
{
    public class ServiceResponse
    {
        public OperationStatus Status { get; set; }

        public int? ErrorCode { get; set; }
    }
}
