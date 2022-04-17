using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Professions.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Professions.Http;

[PublicAPI]
public sealed class ProfessionNamesRequest : IHttpRequest<IReplicaSet<ProfessionName>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplicaSet<ProfessionName>> SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        using var response = await httpClient.SendAsync(Template.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken)
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken)
            .ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetSet(entry => ProfessionNameReader.Read(entry, MissingMemberBehavior));
        return new ReplicaSet<ProfessionName>(response.Headers.Date.GetValueOrDefault(),
            value,
            response.Headers.GetCollectionContext(),
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified);
    }
}
