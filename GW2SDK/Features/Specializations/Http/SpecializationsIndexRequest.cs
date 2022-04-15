using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Specializations.Http;

[PublicAPI]
public sealed class SpecializationsIndexRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/specializations")
    {
        AcceptEncoding = "gzip"
    };

    public static implicit operator HttpRequestMessage(SpecializationsIndexRequest _) => Template.Compile();
}
