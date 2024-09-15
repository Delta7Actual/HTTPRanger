using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRanger.src
{
    public class HTTPRangerException : Exception
    {
        public int StatusCode { get; private set; }

        public HTTPRangerException(int statusCode) : base(GetErrorMessageByCode(statusCode))
        { 
            this.StatusCode = statusCode;
        }
        public HTTPRangerException(string? message) : base(message) 
        {
            this.StatusCode = 999;
        }

        private static string GetErrorMessageByCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request: The server could not understand the request due to invalid syntax.",
                401 => "Unauthorized: Authentication is required and has failed or has not yet been provided.",
                403 => "Forbidden: The server understood the request, but refuses to authorize it.",
                404 => "Not Found: The requested resource could not be found.",
                500 => "Internal Server Error: The server encountered an unexpected condition.",
                502 => "Bad Gateway: The server, while acting as a gateway or proxy, received an invalid response.",
                503 => "Service Unavailable: The server is currently unable to handle the request.",
                504 => "Gateway Timeout: The server, while acting as a gateway or proxy, did not receive a timely response.",
                999 => "External error unrelated to HTTPRanger occured.",
                _ => "Unknown error occurred."
            };
        }
    }
}
