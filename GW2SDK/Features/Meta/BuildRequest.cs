using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Meta;

[PublicAPI]
public sealed class BuildRequest : IHttpRequest<IReplica<Build>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/build")
    {
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        return new Replica<Build>
        {
            Value = json.RootElement.GetBuild(MissingMemberBehavior),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
