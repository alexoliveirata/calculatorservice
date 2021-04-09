using System;

namespace CalculatorService.Server.ExceptionHandler
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException(string message) : base(message)
        { }
    }
}
