using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRanger.src
{
    internal class HttpRequestHelper
    {
        // Builds an HttpRequestMessage with the specified parameters
        public HttpRequestMessage BuildRequest(HttpMethod method, string url, string? content = null, RequestOptions? options = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage();
            requestMessage.Method = method;
            requestMessage.RequestUri = new Uri(url);

            if (content != null)
            {
                // Create HttpContent from the provided content string
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            // Handle custom request options like headers
            if (options?.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    requestMessage.Headers.Add(header.Key, header.Value);
                }
            }

            return requestMessage;
        }

        // Applies headers from RequestOptions to the HttpRequestMessage
        public void ApplyHeaders(HttpRequestMessage request, RequestOptions options)
        {
            // TODO: Implement method to add headers to HttpRequestMessage
            throw new NotImplementedException();
        }

        // Prepares content for HttpRequestMessage (optional method)
        private HttpContent PrepareContent(string content, string mediaType)
        {
            // TODO: Implement method to prepare HttpContent
            throw new NotImplementedException();
        }
    }
}