using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions.Http;

[PublicAPI]
public sealed class ProfessionNamesRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(ProfessionNamesRequest _) => Template.Compile();
}
