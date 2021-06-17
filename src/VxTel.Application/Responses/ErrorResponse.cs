using System.Collections.Generic;

namespace VxTel.Application.Responses
{
    public class ErrorResponse<T>
    {
        public ErrorResponse()
        {
        }
        public ErrorResponse(string message, int statusCode)
        { 
            Message = message;
            StatusCode = statusCode;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
