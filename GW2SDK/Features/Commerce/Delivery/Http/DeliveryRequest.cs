﻿using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Commerce.Delivery.Http
{
    [PublicAPI]
    public sealed class DeliveryRequest
    {
        private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/commerce/delivery");

        public DeliveryRequest(string? accessToken)
        {
            AccessToken = accessToken;
        }

        public string? AccessToken { get; }

        public static implicit operator HttpRequestMessage(DeliveryRequest r)
        {
            var request = Template with
            {
                BearerToken = r.AccessToken
            };
            return request.Compile();
        }
    }
}