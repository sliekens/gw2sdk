using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentByIdRequest
    {
        public ContinentByIdRequest(int continentId, Language? language)
        {
            ContinentId = continentId;
            Language = language;
        }

        public int ContinentId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ContinentByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ContinentId);
            if (r.Language is not null) search.Add("lang", r.Language.ToString());
            var location = new Uri($"/v2/continents?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
