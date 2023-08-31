using CVU.CONDICA.ExceptionHandling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.ExceptionHandling.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(FailureReason failureReason, string message = null)
        {
            FailureReason = failureReason;
            Message = message;
        }

        public FailureReason FailureReason { get; }
        public override string Message { get; }
    }
}
