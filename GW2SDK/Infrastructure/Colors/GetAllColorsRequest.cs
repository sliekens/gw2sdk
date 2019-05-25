using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetAllColorsRequest : HttpRequestMessage
    {
        public GetAllColorsRequest()
            : base(HttpMethod.Get, Resource)
        {
        }

        public static string Resource => "/v2/colors?ids=all";
    }
}