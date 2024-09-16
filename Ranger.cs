using HTTPRanger.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRanger
{
    public static class Ranger
    {
        /// <summary>
        /// Executes a GET request asynchronosely to chosen URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns>HTTPRangerResponse for the request</returns>
        public static async Task<HTTPRangerResponse> GetAsync(string url, RequestOptions? options = null)
        {
            try
            {
                if (!IsURLValid(url))
                    throw new HTTPRangerException(404);
                else
                    return await HTTPRangerWrapper.GetAsync(url, options);
            }
            catch (HttpRequestException)
            {
                //DNS resolution errors
                throw new HTTPRangerException(404);
            }
        }

        /// <summary>
        /// Executes a POST request asynchronosely to chosen URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static async Task<HTTPRangerResponse> PostAsync(string url, string content, RequestOptions? options = null)
        {
            if (!IsURLValid(url))
                throw new HTTPRangerException(999);
            else
                return await HTTPRangerWrapper.PostAsync(url, content, options);
        }

        /// <summary>
        /// Checks if a URL is valid
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static bool IsURLValid(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
