using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Mounts.Http;

internal sealed class MountsByNamesRequest : IHttpRequest<HashSet<Mount>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/mounts/types")
    {
        AcceptEncoding = "gzip"
    };

    public MountsByNamesRequest(IReadOnlyCollection<MountName> mountNames)
    {
        Check.Collection(mountNames);
        MountNames = mountNames;
    }

    public IReadOnlyCollection<MountName> MountNames { get; }

    public Language? Language { get; init; }

    
    public async Task<(HashSet<Mount> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", MountNames.Select(MountNameFormatter.FormatMountName) },
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
        var value = json.RootElement.GetSet(static entry => entry.GetMount());
        return (value, new MessageContext(response));
    }
}
