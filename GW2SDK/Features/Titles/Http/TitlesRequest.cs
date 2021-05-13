﻿using System;
using System.Net.Http;
using GW2SDK.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Titles.Http
{
    [PublicAPI]
    public sealed class TitlesRequest
    {
        public static implicit operator HttpRequestMessage(TitlesRequest _)
        {
            var location = new Uri("/v2/titles?ids=all", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}