using System;
using System.Transactions;

namespace Questionnaire.Service.Infrastructure
{
    public class ServiceCommand : ServiceCommandBase
    {
        public Action<ServiceCommand, object> Execution { get; set; }

        public void Execute(object parameter)
        {
            if(this.CanExecute(parameter) && this.Execution != null)
            {
                var tscOptions = new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted };
                using (var ts = new TransactionScope(TransactionScopeOption.Required, tscOptions))
                {
                    try
                    {
                        this.Execution.Invoke(this, parameter);

                        ts.Complete();
                    }
                    catch(CustomException cex)
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
