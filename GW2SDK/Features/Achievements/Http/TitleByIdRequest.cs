using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Json;
using GW2SDK.Achievements.Models;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Achievements.Http;

[PublicAPI]
public sealed class TitleByIdRequest : IHttpRequest<IReplica<Title>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/titles")
    {
        AcceptEncoding = "gzip"
    };

    public TitleByIdRequest(int titleId)
    {
        TitleId = titleId;
    }

    public int TitleId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Title>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", TitleId);
        var request = Template with { Arguments = search };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = TitleReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Title>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
