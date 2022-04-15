using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Mounts.Http;

[PublicAPI]
public sealed class MountSkinsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/mounts/skins")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(MountSkinsIndexRequest _) => Template.Compile();
}
