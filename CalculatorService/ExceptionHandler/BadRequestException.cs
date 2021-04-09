using System;

namespace CalculatorService.Server.ExceptionHandler
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
