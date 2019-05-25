using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldIdsRequest : HttpRequestMessage
    {
        public GetWorldIdsRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/worlds";
    }
}