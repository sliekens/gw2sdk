﻿using System;
using System.Net.Http;
using GW2SDK.Impl;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Traits.Impl
{
    public sealed class TraitByIdRequest
    {
        public TraitByIdRequest(int traitId)
        {
            TraitId = traitId;
        }

        public int TraitId { get; }

        public static implicit operator HttpRequestMessage(TraitByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.TraitId);
            var location = new Uri($"/v2/traits?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
