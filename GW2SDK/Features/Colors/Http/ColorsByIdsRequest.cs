using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Colors.Http
{
    [PublicAPI]
    public sealed class ColorsByIdsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/colors")
        {
            AcceptEncoding = "gzip"
        };

        public ColorsByIdsRequest(IReadOnlyCollection<int> colorIds, Language? language)
        {
            Check.Collection(colorIds, nameof(colorIds));
            ColorIds = colorIds;
            Language = language;
        }

        public IReadOnlyCollection<int> ColorIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ColorsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.ColorIds);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
