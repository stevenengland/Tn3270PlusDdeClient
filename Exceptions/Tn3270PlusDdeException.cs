using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tn3270PlusDde.Exceptions
{
    public class Tn3270PlusDdeException : Exception
    {
        public string ErrorMessage { get; internal set; }

        public Tn3270PlusDdeException(string message) : base(message)
        {
        }

        public Tn3270PlusDdeException(string message, string errorMessage) : base(message)
        {
            ErrorMessage = errorMessage;
        }
        public Tn3270PlusDdeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Tn3270PlusDdeException(string message, string errorMessage, Exception innerException) : base(message, innerException)
        {
            ErrorMessage = errorMessage;
        }
    }
}
