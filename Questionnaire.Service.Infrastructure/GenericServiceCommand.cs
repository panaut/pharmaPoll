using System;
using System.Transactions;

namespace Questionnaire.Service.Infrastructure
{
    public class ServiceCommand<T> : ServiceCommand
    {
        public new ServiceResponse<T> Result { get; set; }

        public override OperationStatus Status
        {
            get { return this.Result.Status; }
            set { this.Result.Status = value; }
        }

        public ServiceCommand()
        {
            this.Result = new ServiceResponse<T>();
        }

        public T OperationResult { get; protected set; }

        public new Func<ServiceCommand<T>, object, T> Execution { get; set; }

        public new void Execute(object parameter)
        {
            if (this.CanExecute(parameter) && this.Execution != null)
            {
                var tscOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, tscOptions))
                {
                    try
                    {
                        var result = this.Execution.Invoke(this, parameter);
                        this.Result.OperationResult = result;

                        ts.Complete();
                    }
                    catch (CustomException cex)
                    {
                        this.Exception = cex;
                        this.Result.ErrorCode = cex.ErrorCode;
                    }
                    catch (Exception ex)
                    {
                        this.Status = OperationStatus.Failure;
                        this.Exception = new CustomException(ex.Message, ex);
                    }
                }
            }
        }
    }
}
