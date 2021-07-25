using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries.Http
{
    [PublicAPI]
    public sealed class MasteryByIdRequest
    {
        public MasteryByIdRequest(int masteryId, Language? language)
        {
            MasteryId = masteryId;
            Language = language;
        }

        public int MasteryId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MasteryByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MasteryId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/masteries?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
