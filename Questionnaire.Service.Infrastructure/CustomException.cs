using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Questionnaire.Service.Infrastructure
{
    public class CustomException : Exception
    {
        private ILogger logger = LogManager.GetLogger("CustomException");

        public int ErrorCode { get; set; }

        public CustomException(string message, Exception originalException, int errorCode = 0)
            : base(message, originalException)
        {
            logger.Error(message);
            logger.Error(originalException);
            this.ErrorCode = errorCode;
        }

        public CustomException(Exception originalException, int errorCode = 0)
            : this(originalException.Message, originalException, errorCode)
        {

        }
    }
}
