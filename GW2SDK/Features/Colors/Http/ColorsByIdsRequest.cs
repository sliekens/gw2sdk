using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Colors.Http;

internal sealed class ColorsByIdsRequest : IHttpRequest<HashSet<Dye>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/colors")
    {
        AcceptEncoding = "gzip"
    };

    public ColorsByIdsRequest(IReadOnlyCollection<int> colorIds)
    {
        Check.Collection(colorIds);
        ColorIds = colorIds;
    }

    public IReadOnlyCollection<int> ColorIds { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Dye> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", ColorIds },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetDye(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
