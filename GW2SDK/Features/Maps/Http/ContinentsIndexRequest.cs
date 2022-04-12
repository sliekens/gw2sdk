using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Maps.Http;

[PublicAPI]
public sealed class ContinentsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/continents");

    public static implicit operator HttpRequestMessage(ContinentsIndexRequest _)
    {
        return Template.Compile();
    }
}