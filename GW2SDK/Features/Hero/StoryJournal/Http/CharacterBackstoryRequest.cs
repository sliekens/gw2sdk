using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Http;

namespace GuildWars2.Hero.StoryJournal.Http;

internal sealed class CharacterBackstoryRequest(string characterName) : IHttpRequest<CharacterBackstory>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/backstory")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(CharacterBackstory Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetCharacterBackstory(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}