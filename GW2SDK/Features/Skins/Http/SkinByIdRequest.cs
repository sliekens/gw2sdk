using System;
using System.Net.Http;
using JetBrains.Annotations;
using GW2SDK.Http;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinByIdRequest
    {
        public SkinByIdRequest(int skinId)
        {
            SkinId = skinId;
        }

        public int SkinId { get; }

        public static implicit operator HttpRequestMessage(SkinByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.SkinId);
            var location = new Uri($"/v2/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
