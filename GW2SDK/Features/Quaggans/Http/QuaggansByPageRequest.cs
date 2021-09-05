using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Quaggans.Http
{
    [PublicAPI]
    public sealed class QuaggansByPageRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/quaggans")
        {
            AcceptEncoding = "gzip"
        };

        public QuaggansByPageRequest(int pageIndex, int? pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; }

        public int? PageSize { get; }

        public static implicit operator HttpRequestMessage(QuaggansByPageRequest r)
        {
            var search = new QueryBuilder();
            search.Add("page", r.PageIndex);
            if (r.PageSize.HasValue) search.Add("page_size", r.PageSize.Value);
            var request = Template with
            {
                Arguments = search
            };
            return request.Compile();
        }
    }
}
