using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Quaggans.Http;

[PublicAPI]
public sealed class QuagganByIdRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public QuagganByIdRequest(string quagganId)
    {
        QuagganId = quagganId;
    }

    public string QuagganId { get; }

    public static implicit operator HttpRequestMessage(QuagganByIdRequest r)
    {
        QueryBuilder search = new();
        search.Add("id", r.QuagganId);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}