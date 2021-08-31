using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;

namespace GW2SDK.Professions.Http
{
    [PublicAPI]
    public sealed class ProfessionByNameRequest
    {
        public ProfessionByNameRequest(ProfessionName professionName, Language? language)
        {
            Check.Constant(professionName, nameof(professionName));
            ProfessionName = professionName;
            Language = language;
        }

        public ProfessionName ProfessionName { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ProfessionByNameRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ProfessionName.ToString());
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/professions?{search}", UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}
