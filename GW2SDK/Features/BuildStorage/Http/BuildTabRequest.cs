using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.BuildStorage.Http;

internal sealed class BuildTabRequest : IHttpRequest<Replica<BuildTab>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/buildtabs/:tab") { AcceptEncoding = "gzip" };

    public BuildTabRequest(string characterName, int tab)
    {
        CharacterName = characterName;
        Tab = tab;
    }

    public string CharacterName { get; }

    public int Tab { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<BuildTab>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName)
                        .Replace(":tab", Tab.ToString(CultureInfo.InvariantCulture)),
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
        return new Replica<BuildTab>
        {
            Value = json.RootElement.GetBuildTab(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
