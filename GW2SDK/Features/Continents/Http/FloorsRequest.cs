using System;
using System.Globalization;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents/:id/floors")
        {
            AcceptEncoding = "gzip"
        };

        public FloorsRequest(int continentId, Language? language)
        {
            ContinentId = continentId;
            Language = language;
        }

        public int ContinentId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(FloorsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", "all");
            var request = Template with
            {
                Path = Template.Path.Replace(":id", r.ContinentId.ToString(CultureInfo.InvariantCulture)),
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
