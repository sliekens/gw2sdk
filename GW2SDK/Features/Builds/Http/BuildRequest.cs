using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Builds.Http;

[PublicAPI]
public sealed class BuildRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/build");

    public static implicit operator HttpRequestMessage(BuildRequest _)
    {
        return Template.Compile();
    }
}