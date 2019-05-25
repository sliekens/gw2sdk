using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetAllWorldsRequest : HttpRequestMessage
    {
        public GetAllWorldsRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/worlds?ids=all";
    }
}
