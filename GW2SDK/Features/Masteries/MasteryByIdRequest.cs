using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Masteries;

[PublicAPI]
public sealed class MasteryByIdRequest : IHttpRequest<IReplica<Mastery>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/v2/masteries")
    {
        AcceptEncoding = "gzip"
    };

    public MasteryByIdRequest(int masteryId)
    {
        MasteryId = masteryId;
    }

    public int MasteryId { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Mastery>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        search.Add("id", MasteryId);
        var request = Template with
        {
            Arguments = search,
            AcceptLanguage = Language?.Alpha2Code
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = MasteryReader.Read(json.RootElement, MissingMemberBehavior);
        return new Replica<Mastery>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
