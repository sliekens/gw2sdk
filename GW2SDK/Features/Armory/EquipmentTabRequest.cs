using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Armory;

[PublicAPI]
public sealed class EquipmentTabRequest : IHttpRequest<Replica<EquipmentTab>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/characters/:id/aquipmenttabs/:tab")
    {
        AcceptEncoding = "gzip"
    };

    public EquipmentTabRequest(string characterName, int tab)
    {
        CharacterName = characterName;
        Tab = tab;
    }

    public string CharacterName { get; }

    public int Tab { get; }

    public string? AccessToken { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<EquipmentTab>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path
                        .Replace(":id", CharacterName)
                        .Replace(":tab", Tab.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder
                    {
                        { "v", SchemaVersion.Recommended }
                    },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        return new Replica<EquipmentTab>
        {
            Value = json.RootElement.GetEquipmentTab(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
