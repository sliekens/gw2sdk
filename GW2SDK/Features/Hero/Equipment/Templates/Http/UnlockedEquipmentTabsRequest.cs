﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Templates.Http;

internal sealed class UnlockedEquipmentTabsRequest(string characterName)
    : IHttpRequest<IReadOnlyList<int>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/equipmenttabs")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public string CharacterName { get; } = characterName;

    public required string? AccessToken { get; init; }

    public async Task<(IReadOnlyList<int> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(":id", CharacterName),
                    BearerToken = AccessToken
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetList(entry => entry.GetInt32());
        return (value, new MessageContext(response));
    }
}
