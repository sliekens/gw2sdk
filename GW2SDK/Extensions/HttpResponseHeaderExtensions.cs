using System.Linq;
using System.Net.Http.Headers;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;

namespace GW2SDK.Extensions
{
    public static class HttpResponseHeaderExtensions
    {
        public static ListMetaData GetListMetaData(this HttpResponseHeaders headers)
        {
            var data = new ListMetaData();
            if (headers.TryGetValues(ResponseHeaderName.ResultCount, out var counts) &&
                int.TryParse(counts.SingleOrDefault(), out var count))
            {
                data.ResultCount = count;
            }

            if (headers.TryGetValues(ResponseHeaderName.ResultTotal, out var totals) &&
                int.TryParse(totals.SingleOrDefault(), out var total))
            {
                data.ResultTotal = total;
            }

            return data;
        }
    }
}
