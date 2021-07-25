using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitleByIdRequest
    {
        public TitleByIdRequest(int titleId, Language? language)
        {
            TitleId = titleId;
            Language = language;
        }

        public int TitleId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(TitleByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.TitleId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/titles?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
