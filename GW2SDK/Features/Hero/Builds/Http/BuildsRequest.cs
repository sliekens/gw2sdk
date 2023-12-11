using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class BuildsRequest(string characterName) : IHttpRequest<HashSet<BuildTemplate>>
{
    // There is no ids=all support, but page=0 works
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/buildtabs?page=0") { AcceptEncoding = "gzip" };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<BuildTemplate> Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetBuildTemplate(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
