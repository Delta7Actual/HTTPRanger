using System.Collections.Generic;
using System.Net.Http;

namespace HTTPRanger.src
{
    public class HTTPRangerResponse
    {
        /// <summary>
        /// Response Properties
        /// </summary>
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Constructor for HTTPRangerResponse
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <param name="isSuccess"></param>
        public HTTPRangerResponse(int statusCode, string content, Dictionary<string, string> headers, bool isSuccess)
        {
            StatusCode = statusCode;
            Content = content;
            Headers = headers;
            IsSuccess = isSuccess;
        }
    }
}
