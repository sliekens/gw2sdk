using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorByIdRequest : HttpRequestMessage
    {
        public GetColorByIdRequest(int colorId)
            : base(HttpMethod.Get, GetResource(colorId))
        {
        }

        public static string GetResource(int colorId) => $"/v2/colors?id={colorId}";
    }
}