using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HTTPRanger.src
{
    internal class HttpRequestHelper
    {
        /// <summary>
        /// Builds an HttpRequestMessage with the specified parameters
        /// </summary>
        /// <param name="method">HTTP method (GET, POST, etc.)</param>
        /// <param name="url">Request URL</param>
        /// <param name="content">Request body content</param>
        /// <param name="options">Request options including headers</param>
        /// <returns>HttpRequestMessage</returns>
        public HttpRequestMessage BuildRequest(HttpMethod method, string url, string? content = null, RequestOptions? options = null)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(url)
            };

            if (content != null)
            {
                // Create HttpContent from the provided content string
                var httpContent = PrepareContent(content, "application/json");

                // Apply content headers if present
                if (options?.Headers != null && options.Headers.ContainsKey("Content-Type"))
                {
                    httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(options.Headers["Content-Type"]);
                }

                requestMessage.Content = httpContent;
            }

            if (options != null)
                ApplyHeaders(requestMessage, options);

            return requestMessage;
        }

        /// <summary>
        /// Applies headers from RequestOptions to the HttpRequestMessage
        /// </summary>
        /// <param name="request">HttpRequestMessage to apply headers to</param>
        /// <param name="options">Request options including headers</param>
        private void ApplyHeaders(HttpRequestMessage request, RequestOptions options)
        {
            if (options.Headers != null)
            {
                foreach (var header in options.Headers)
                {
                    // Check if header should be added to the request or the content
                    if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                    {
                        // Content-Type should be set on HttpContent, not on HttpRequestMessage.Headers
                        continue;
                    }

                    // Add general headers to the request
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Prepares content for HttpRequestMessage
        /// </summary>
        /// <param name="content">Content string</param>
        /// <param name="mediaType">Media type of the content</param>
        /// <returns>HttpContent</returns>
        private HttpContent PrepareContent(string content, string mediaType)
        {
            return new StringContent(content, Encoding.UTF8, mediaType);
        }
    }
}
