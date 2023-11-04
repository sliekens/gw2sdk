using GuildWars2.Http;

namespace GuildWars2.Crafting.Http;

internal sealed class
    LearnedCraftingDisciplinesRequest : IHttpRequest2<LearnedCraftingDisciplines>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/crafting")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public LearnedCraftingDisciplinesRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(LearnedCraftingDisciplines Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetLearnedCraftingDisciplines(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
