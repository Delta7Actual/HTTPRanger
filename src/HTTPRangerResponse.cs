using System.Collections.Generic;
using System.Net.Http;

namespace HTTPRanger.src
{
    public class HTTPRangerResponse
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public bool IsSuccess { get; set; }

        // Constructor for HTTPRangerResponse
        public HTTPRangerResponse(int statusCode, string content, Dictionary<string, string> headers, bool isSuccess)
        {
            StatusCode = statusCode;
            Content = content;
            Headers = headers;
            IsSuccess = isSuccess;
        }
    }
}
