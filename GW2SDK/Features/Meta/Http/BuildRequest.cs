using GuildWars2.Http;

namespace GuildWars2.Meta.Http;

internal sealed class BuildRequest : IHttpRequest<Replica<Build>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/build")
    {
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Build>> SendAsync(
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
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
