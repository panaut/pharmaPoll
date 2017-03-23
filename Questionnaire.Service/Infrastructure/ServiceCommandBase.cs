using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Infrastructure
{
    internal abstract class ServiceCommandBase
    {
        public ServiceCommandBase()
        {
            this.Result = new ServiceResponse();
        }

        public ServiceResponse Result { get; set; }

        public OperationStatus Status
        {
            get { return this.Result.Status; }
            set { this.Result.Status = value; }
        }

        public Exception Exception { get; set; }

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }
    }
}
