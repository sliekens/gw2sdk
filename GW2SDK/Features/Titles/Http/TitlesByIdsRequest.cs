using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitlesByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/titles")
        {
            AcceptEncoding = "gzip"
        };

        public TitlesByIdsRequest(IReadOnlyCollection<int> titleIds, Language? language)
        {
            Check.Collection(titleIds, nameof(titleIds));
            TitleIds = titleIds;
            Language = language;
        }

        public IReadOnlyCollection<int> TitleIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(TitlesByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.TitleIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
