using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldsByPageRequest : HttpRequestMessage
    {
        public GetWorldsByPageRequest(int page, int? pageSize)
            : base(HttpMethod.Get, GetResource(page, pageSize))
        {
        }

        public static string GetResource(int page, int? pageSize)
        {
            var resource = $"/v2/worlds?page={page}";
            if (pageSize.HasValue)
            {
                resource += $"&page_size={pageSize.Value}";
            }

            return resource;
        }
    }
}
