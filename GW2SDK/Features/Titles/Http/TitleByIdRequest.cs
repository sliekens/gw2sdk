using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitleByIdRequest
    {
        public TitleByIdRequest(int titleId)
        {
            TitleId = titleId;
        }

        public int TitleId { get; }

        public static implicit operator HttpRequestMessage(TitleByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.TitleId);
            var location = new Uri($"/v2/titles?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
