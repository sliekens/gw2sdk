using System.Globalization;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class FloorsIndexRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents/:id/floors")
        {
            AcceptEncoding = "gzip"
        };

        public FloorsIndexRequest(int continentId)
        {
            ContinentId = continentId;
        }

        public int ContinentId { get; }

        public static implicit operator HttpRequestMessage(FloorsIndexRequest r)
        {
            var request = Template with
            {
                Path = Template.Path.Replace(":id", r.ContinentId.ToString(CultureInfo.InvariantCulture))
            };
            return request.Compile();
        }
    }
}
