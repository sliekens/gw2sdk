using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Accounts.Http;

internal sealed class CharactersRequest : IHttpRequest<HashSet<Character>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder
        {
            { "ids", "all" },
            { "v", SchemaVersion.Recommended }
        }
    };

    public required string? AccessToken { get; init; }

    
    public async Task<(HashSet<Character> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { BearerToken = AccessToken },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(static entry => entry.GetCharacter());
        return (value, new MessageContext(response));
    }
}
