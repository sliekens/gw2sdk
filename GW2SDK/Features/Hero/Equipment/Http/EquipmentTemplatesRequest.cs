﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Http;

internal sealed class EquipmentTemplatesRequest : IHttpRequest<HashSet<EquipmentTemplate>>
{
    // There is no ids=all support, but page=0 works
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/equipmenttabs?page=0") { AcceptEncoding = "gzip" };

    public EquipmentTemplatesRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<EquipmentTemplate> Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetSet(entry => EquipmentTemplateJson.GetEquipmentTemplate(entry, MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}