using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVU.CONDICA.ExceptionHandling.Exceptions
{
    public class RequestValidationFailedException : ApplicationException
    {
        public RequestValidationFailedException(IEnumerable<string> failures)
        {
            Failures = failures;
        }
        public IEnumerable<string> Failures { private set; get; }
    }
}
