using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountSkinByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public MountSkinByIdRequest(int mountSkinId, Language? language)
    {
        MountSkinId = mountSkinId;
        Language = language;
    }

    public int MountSkinId { get; }

    public Language? Language { get; }

    public static implicit operator HttpRequestMessage(MountSkinByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.MountSkinId);
        var request = Template with
        {
            AcceptLanguage = r.Language?.Alpha2Code,
            Arguments = search
        };
        return request.Compile();
    }
}