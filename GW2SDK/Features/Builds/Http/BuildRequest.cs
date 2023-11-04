using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Builds.Http;

internal sealed class BuildRequest : IHttpRequest<BuildTemplate>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/buildtabs/:tab") { AcceptEncoding = "gzip" };

    public BuildRequest(string characterName, int tabNumber)
    {
        CharacterName = characterName;
        TabNumber = tabNumber;
    }

    public string CharacterName { get; }

    public int TabNumber { get; }

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
                    Path = Template.Path.Replace(":id", CharacterName).Replace(":tab", TabNumber.ToString(CultureInfo.InvariantCulture)),
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
