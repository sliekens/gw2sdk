using GuildWars2.Http;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class ActiveBuildRequest : IHttpRequest<BuildTemplate>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/buildtabs/active") { AcceptEncoding = "gzip" };

    public ActiveBuildRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(BuildTemplate Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetBuildTemplate(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
