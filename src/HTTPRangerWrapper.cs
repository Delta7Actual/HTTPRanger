﻿using System;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Sockets;

namespace HTTPRanger.src
{
    internal static class HTTPRangerWrapper
    {
        /// <summary>
        /// Static helper objects
        /// </summary>
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly HttpRequestHelper _requestHelper = new HttpRequestHelper();

        /// <summary>
        /// Sends a GET request to the specified URL with content and optional request options
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="options"></param>
        /// <returns>Task<HTTPRangerResponse></returns>
        public static async Task<HTTPRangerResponse> GetAsync(string url, RequestOptions? options = null)
        {
            try
            {
                var request = _requestHelper.BuildRequest(HttpMethod.Get, url, null, options);
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);

                HTTPRangerResponse responseMessage = await CreateHTTPRangerResponse(httpResponseMessage);

                if (!responseMessage.IsSuccess) throw new HTTPRangerException(responseMessage.StatusCode);
                return responseMessage;
            }
            // DNS resolution error or unreachable host
            catch (HttpRequestException)
            {
                throw new HTTPRangerException(404);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Internal Server Error: The server encountered an unexpected condition.")
                    throw new HTTPRangerException(500);
                else
                    throw new HTTPRangerException(ex.Message);
            }
        }

        /// <summary>
        /// Sends a POST request to the specified URL with content and optional request options
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="options"></param>
        /// <returns>Task<HTTPRangerResponse></returns>
        public static async Task<HTTPRangerResponse> PostAsync(string url, string content, RequestOptions? options = null)
        {
            try
            {
                var request = _requestHelper.BuildRequest(HttpMethod.Post, url, content, options);
                HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(request);

                HTTPRangerResponse responseMessage = await CreateHTTPRangerResponse(httpResponseMessage);

                if (!responseMessage.IsSuccess) throw new HTTPRangerException(responseMessage.StatusCode);

                return responseMessage;
            }
            catch (HttpRequestException)
            {
                throw new HTTPRangerException(404);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Internal Server Error: The server encountered an unexpected condition.")
                    throw new HTTPRangerException(500);
                else
                    throw new HTTPRangerException(ex.Message);
            }
        }

        /// <summary>
        /// Converts HttpResponseMessage to custom HTTPRangerResponse
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns>Task<HTTPRangerResponse></returns>
        private static async Task<HTTPRangerResponse> CreateHTTPRangerResponse(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                string? content = await httpResponseMessage.Content.ReadAsStringAsync();
                Dictionary<string, string> headers = httpResponseMessage.Headers.ToDictionary(
                    h => h.Key,
                    h => string.Join(",", h.Value)
                );

                HTTPRangerResponse responseMessage = new HTTPRangerResponse(
                    statusCode: (int)httpResponseMessage.StatusCode,
                    content: content,
                    headers: headers,
                    isSuccess: httpResponseMessage.IsSuccessStatusCode
                );

                return responseMessage;
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }
        }
    }
}
