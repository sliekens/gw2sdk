﻿using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Continents.Http
{
    [PublicAPI]
    public sealed class ContinentByIdRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents")
        {
            AcceptEncoding = "gzip"
        };

        public ContinentByIdRequest(int continentId, Language? language)
        {
            ContinentId = continentId;
            Language = language;
        }

        public int ContinentId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(ContinentByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.ContinentId);
            var request = Template with
            {
                AcceptLanguage = r.Language?.Alpha2Code,
                Arguments = search
            };
            return request.Compile();
        }
    }
}
