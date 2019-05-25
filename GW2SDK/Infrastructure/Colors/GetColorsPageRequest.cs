using System.Net.Http;

namespace GW2SDK.Infrastructure.Colors
{
    public sealed class GetColorsPageRequest : HttpRequestMessage
    {
        public GetColorsPageRequest(int page, int? pageSize)
            : base(HttpMethod.Get, GetResource(page, pageSize))
        {
        }

        public static string GetResource(int page, int? pageSize)
        {
            var resource = $"/v2/colors?page={page}";
            if (pageSize.HasValue)
            {
                resource += $"&page_size={pageSize.Value}";
            }

            return resource;
        }
    }
}
