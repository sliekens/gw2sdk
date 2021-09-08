using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    // TODO: revalidate support
    [PublicAPI]
    public sealed class CacheResponse
    {
        public static readonly CacheResponse Miss = new(false, null);

        private CacheResponse(bool hit, HttpResponseMessage? response)
        {
            Hit = hit;
            Response = response;
        }

        [MemberNotNullWhen(true, nameof(Response))]
        public bool Hit { get; }

        public HttpResponseMessage? Response { get; }

        public static CacheResponse CreateHit(HttpResponseMessage response) => new(true, response);
    }
}
