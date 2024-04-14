﻿using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Templates.Http;

internal sealed class CharacterEquipmentRequest(string characterName)
    : IHttpRequest<CharacterEquipment>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/equipment") { AcceptEncoding = "gzip" };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    
    public async Task<(CharacterEquipment Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetCharacterEquipment();
        return (value, new MessageContext(response));
    }
}
