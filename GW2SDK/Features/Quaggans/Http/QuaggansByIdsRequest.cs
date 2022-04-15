using System.Collections.Generic;
using System.Net.Http;
using GW2SDK.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Quaggans.Http;

[PublicAPI]
public sealed class QuaggansByIdsRequest
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/quaggans")
    {
        AcceptEncoding = "gzip"
    };

    public QuaggansByIdsRequest(IReadOnlyCollection<string> quagganIds)
    {
        Check.Collection(quagganIds, nameof(quagganIds));
        QuagganIds = quagganIds;
    }

    public IReadOnlyCollection<string> QuagganIds { get; }

    public static implicit operator HttpRequestMessage(QuaggansByIdsRequest r)
    {
        QueryBuilder search = new();
        search.Add("ids", r.QuagganIds);
        var request = Template with
        {
            Arguments = search
        };
        return request.Compile();
    }
}
