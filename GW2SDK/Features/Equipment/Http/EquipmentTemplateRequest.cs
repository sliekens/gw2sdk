﻿using System.Globalization;
using GuildWars2.Http;

namespace GuildWars2.Equipment.Http;

internal sealed class EquipmentTemplateRequest : IHttpRequest<Replica<EquipmentTemplate>>
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

    public async Task<Replica<EquipmentTemplate>> SendAsync(
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
        return new Replica<EquipmentTemplate>
        {
            Value = value,
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
