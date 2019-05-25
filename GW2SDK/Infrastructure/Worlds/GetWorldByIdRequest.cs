using System.Net.Http;

namespace GW2SDK.Infrastructure.Worlds
{
    public sealed class GetWorldByIdRequest : HttpRequestMessage
    {
        public GetWorldByIdRequest(int worldId)
            : base(HttpMethod.Get, GetResource(worldId))
        {
        }

        public static string GetResource(int worldId) => $"/v2/worlds?id={worldId}";
    }
}