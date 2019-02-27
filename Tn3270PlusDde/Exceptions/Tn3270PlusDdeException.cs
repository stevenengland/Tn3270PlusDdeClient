using System;

namespace StEn.Tn3270PlusDde.Exceptions
{
    public class Tn3270PlusDdeException : Exception
    {
        public Tn3270PlusDdeException(string message)
            : base(message)
        {
        }

        public Tn3270PlusDdeException(string message, string errorMessage)
            : base(message)
        {
            this.ErrorMessage = errorMessage;
        }

        public Tn3270PlusDdeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public Tn3270PlusDdeException(string message, string errorMessage, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; internal set; }
    }
}
