using System.Collections.Generic;
using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountSkinsByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public MountSkinsByIdsRequest(IReadOnlyCollection<int> mountSkinIds, Language? language)
    {
        Check.Collection(mountSkinIds, nameof(mountSkinIds));
        MountSkinIds = mountSkinIds;
        Language = language;
    }

    public IReadOnlyCollection<int> MountSkinIds { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MountSkinsByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.MountSkinIds);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}