using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Crafting.Http;

[PublicAPI]
public sealed class DailyCraftingRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/dailycrafting")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(DailyCraftingRequest _) => Template.Compile();
}
