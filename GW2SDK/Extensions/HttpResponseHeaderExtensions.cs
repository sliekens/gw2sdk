using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace GW2SDK.Extensions
{
    public static class HttpResponseHeaderExtensions
    {
        public static Dictionary<string, string> GetMetaData(this HttpResponseHeaders headers)
        {
            return headers.ToDictionary(header => header.Key, header => header.Value.ToString());
        }
    }
}
