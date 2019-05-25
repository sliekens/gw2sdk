using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorIdsRequest : HttpRequestMessage
    {
        public GetColorIdsRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/colors";
    }
}