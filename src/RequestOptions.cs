using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;

namespace HTTPRanger.src
{
    public class RequestOptions
    {
        public Dictionary<string, string> Headers{ get; set; }

        /// <summary>
        /// Instantiates a new RequestOptions object.
        /// If called without parameters, An empty Dictionary will be created.
        /// </summary>
        /// <param name="headers"></param>
        public RequestOptions(Dictionary<string, string> headers = null)
        {
            if (headers != null)
                this.Headers = headers;
            else
                this.Headers = new Dictionary<string, string>();
        }
    }
}
