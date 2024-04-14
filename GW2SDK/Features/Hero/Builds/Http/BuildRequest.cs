using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Hero.Builds.Http;

internal sealed class BuildRequest(string characterName, int tabNumber)
    : IHttpRequest<BuildTemplate>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/buildtabs/:tab") { AcceptEncoding = "gzip" };

    public string CharacterName { get; } = characterName;

    public int TabNumber { get; } = tabNumber;

    public required string? AccessToken { get; init; }

    
    public async Task<(BuildTemplate Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName)
                        .Replace(":tab", TabNumber.ToString(CultureInfo.InvariantCulture)),
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
        var value = json.RootElement.GetBuildTemplate();
        return (value, new MessageContext(response));
    }
}
