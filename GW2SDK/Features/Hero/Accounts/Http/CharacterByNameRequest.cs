using GuildWars2.Http;

namespace GuildWars2.Hero.Accounts.Http;

internal sealed class CharacterByNameRequest(string characterName) : IHttpRequest<Character>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters")
    {
        AcceptEncoding = "gzip"
    };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    
    public async Task<(Character Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    BearerToken = AccessToken,
                    Arguments = new QueryBuilder
                    {
                        { "id", CharacterName },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetCharacter();
        return (value, new MessageContext(response));
    }
}
