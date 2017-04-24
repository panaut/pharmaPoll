using System;

namespace Questionnaire.Service.Infrastructure
{
    public abstract class ServiceCommandBase
    {
        public ServiceCommandBase()
        {
            this.Result = new ServiceResponse();
        }

        public ServiceResponse Result { get; set; }

        public virtual OperationStatus Status
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
