using System;
using System.Net;

namespace WebJetMovies.Api.ViewModels
{
    public class ErrorModel
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public static ErrorModel Create(HttpStatusCode code, string message)
        {
            return new ErrorModel
            {
                ErrorCode = (int)code,
                Message = message
            };
        }

        public static ErrorModel CreateForInternalServerError(Exception exception)
        {
            return Create(HttpStatusCode.InternalServerError, exception.Message);
        }
    }
}
