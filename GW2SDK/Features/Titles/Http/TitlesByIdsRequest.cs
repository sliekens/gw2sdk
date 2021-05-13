using System;
using System.Collections.Generic;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitlesByIdsRequest
    {
        public TitlesByIdsRequest(IReadOnlyCollection<int> titleIds)
        {
            if (titleIds is null)
            {
                throw new ArgumentNullException(nameof(titleIds));
            }

            if (titleIds.Count == 0)
            {
                throw new ArgumentException("Title IDs cannot be an empty collection.", nameof(titleIds));
            }

            TitleIds = titleIds;
        }

        public IReadOnlyCollection<int> TitleIds { get; }

        public static implicit operator HttpRequestMessage(TitlesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.TitleIds);
            var location = new Uri($"/v2/titles?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
