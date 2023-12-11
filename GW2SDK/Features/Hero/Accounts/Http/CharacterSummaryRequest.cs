using GuildWars2.Http;

namespace GuildWars2.Hero.Accounts.Http;

internal sealed class CharacterSummaryRequest(string characterName) : IHttpRequest<CharacterSummary>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters/:id/core")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(CharacterSummary Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName),
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetCharacterSummary(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
