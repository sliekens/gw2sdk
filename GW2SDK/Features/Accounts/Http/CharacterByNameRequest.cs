using GuildWars2.Http;

namespace GuildWars2.Accounts.Http;

internal sealed class CharacterByNameRequest : IHttpRequest2<Character>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters")
    {
        AcceptEncoding = "gzip"
    };

    public CharacterByNameRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetCharacter(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
