using GuildWars2.Home.Cats;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Home.Http;

internal sealed class CatsRequest : IHttpRequest2<HashSet<Cat>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/home/cats")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Cat> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(Template, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetCat(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
