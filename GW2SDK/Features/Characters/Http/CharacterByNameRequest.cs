using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class CharacterByNameRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters")
        {
            AcceptEncoding = "gzip"
        };

        public CharacterByNameRequest(string characterName, string? accessToken)
        {
            CharacterName = characterName;
            AccessToken = accessToken;
        }

        public string CharacterName { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(CharacterByNameRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.CharacterName);
            var request = Template with
            {
                BearerToken = r.AccessToken,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
