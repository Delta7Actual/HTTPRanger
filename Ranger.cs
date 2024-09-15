using HTTPRanger.src;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public static async Task<HTTPRangerResponse> Get(string url)
        {
            return await HTTPRangerWrapper.GetAsync(url, null);
        }
    }
}
