﻿using GuildWars2.Http;

namespace GuildWars2.Inventories.Http;

internal sealed class InventoryRequest : IHttpRequest2<Baggage>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/characters/:id/inventory")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public InventoryRequest(string characterName)
    {
        CharacterName = characterName;
    }

    public string CharacterName { get; }

    public required string? AccessToken { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Baggage Value, MessageContext Context)> SendAsync(
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
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetBaggage(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
