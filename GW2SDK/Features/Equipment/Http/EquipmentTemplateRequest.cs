using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Equipment.Http;

internal sealed class EquipmentTemplateRequest : IHttpRequest2<EquipmentTemplate>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/equipmenttabs/:tab") { AcceptEncoding = "gzip" };

    public EquipmentTemplateRequest(string characterName, int tab)
    {
        CharacterName = characterName;
        Tab = tab;
    }

    public string CharacterName { get; }

    public int Tab { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(EquipmentTemplate Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName).Replace(":tab", Tab.ToString(CultureInfo.InvariantCulture)),
                    Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } },
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetEquipmentTemplate(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
