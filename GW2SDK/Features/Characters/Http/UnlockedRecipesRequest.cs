﻿using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Characters.Http
{
    [PublicAPI]
    public sealed class UnlockedRecipesRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/characters/:id/recipes")
        {
            AcceptEncoding = "gzip"
        };

        public UnlockedRecipesRequest(string characterName, string? accessToken)
        {
            CharacterName = characterName;
            AccessToken = accessToken;
        }

        public string CharacterName { get; }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(UnlockedRecipesRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken,
                Path = Template.Path.Replace(":id", r.CharacterName)
            };
            return request.Compile();
        }
    }
}
