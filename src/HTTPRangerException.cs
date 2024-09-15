using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRanger.src
{
    public class HTTPRangerException : Exception
    {
        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { get; private set; }

        /// <summary>
        /// Constructor for HTTPRangerException, receives a status code and throws the corresponding error
        /// </summary>
        /// <param name="statusCode"></param>
        public HTTPRangerException(int statusCode) : base(GetErrorMessageByCode(statusCode))
        { 
            this.StatusCode = statusCode;
        }

        /// <summary>
        /// General Constructor for HTTPRangerException
        /// </summary>
        /// <param name="message"></param>
        public HTTPRangerException(string message) : base(message)
        {
            this.StatusCode = 999;
        }

        /// <summary>
        /// Gets a status code and returns the corresponding error message
        /// </summary>
        /// <param name="statusCode"></param>
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
                
                // TO ADD AN ERROR STATUS CODE SIMPLY ADD IT LIKE SO
                // -> CodeStatus => CodeStatusErrorMessage

                _ => "Unknown error occurred."
            };
        }
    }
}
