using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public sealed class BuildByIdRequest : IHttpRequest<IReplica<Build>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(HttpMethod.Get, "v2/account/buildstorage") { AcceptEncoding = "gzip" };

    public BuildByIdRequest(int buildStorageId)
    {
        BuildStorageId = buildStorageId;
    }

    public int BuildStorageId { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<Build>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", BuildStorageId },
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetBuild(MissingMemberBehavior);
        return new Replica<Build>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
        );
    }
}
