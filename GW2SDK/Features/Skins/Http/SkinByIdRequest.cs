using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Skins.Http
{
    [PublicAPI]
    public sealed class SkinByIdRequest
    {
        public SkinByIdRequest(int skinId, Language? language)
        {
            SkinId = skinId;
            Language = language;
        }

        public int SkinId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(SkinByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.SkinId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
