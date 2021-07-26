using System;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountSkinByIdRequest
    {
        public MountSkinByIdRequest(int mountSkinId, Language? language)
        {
            MountSkinId = mountSkinId;
            Language = language;
        }

        public int MountSkinId { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MountSkinByIdRequest r)
        {
            var search = new QueryBuilder();
            search.Add("id", r.MountSkinId);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/mounts/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
