using GuildWars2.Http;

namespace GuildWars2.Mounts.Http;

internal sealed class MountByNameRequest : IHttpRequest<Replica<Mount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountByNameRequest(MountName mountName)
    {
        Check.Constant(mountName);
        MountName = mountName;
    }

    public MountName MountName { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Mount>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", MountNameFormatter.FormatMountName(MountName) },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<Mount>
        {
            Value = json.RootElement.GetMount(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
