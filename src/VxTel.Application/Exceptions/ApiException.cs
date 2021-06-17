using System;

namespace VxTel.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
    }
}
