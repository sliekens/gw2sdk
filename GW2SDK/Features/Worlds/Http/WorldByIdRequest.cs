using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Worlds.Http
{
    [PublicAPI]
    public sealed class WorldByIdRequest
    {
        public WorldByIdRequest(int worldId, Language? language)
        {
            WorldId = worldId;
            Language = language;
        }

        public int WorldId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(WorldByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.WorldId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/worlds?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
