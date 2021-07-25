using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Http
{
    [PublicAPI]
    public sealed class TraitByIdRequest
    {
        public TraitByIdRequest(int traitId, Language? language)
        {
            TraitId = traitId;
            Language = language;
        }

        public int TraitId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(TraitByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.TraitId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/traits?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
