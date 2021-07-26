using System;
using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http
{
    [PublicAPI]
    public sealed class MountSkinsByIdsRequest
    {
        public MountSkinsByIdsRequest(IReadOnlyCollection<int> mountSkinIds, Language? language)
        {
            if (mountSkinIds is null)
            {
                throw new ArgumentNullException(nameof(mountSkinIds));
            }

            if (mountSkinIds.Count == 0)
            {
                throw new ArgumentException("Mount skin IDs cannot be an empty collection.", nameof(mountSkinIds));
            }

            MountSkinIds = mountSkinIds;
            Language = language;
        }

        public IReadOnlyCollection<int> MountSkinIds { get; }

        public Language? Language { get; }

        public static implicit operator HttpRequestMessage(MountSkinsByIdsRequest r)
        {
            var search = new QueryBuilder();
            search.Add("ids", r.MountSkinIds);
            if (r.Language is not null) search.Add("lang", r.Language.Alpha2Code);
            var location = new Uri($"/v2/mounts/skins?{search}", UriKind.Relative);
            return new HttpRequestMessage(Get, location);
        }
    }
}
