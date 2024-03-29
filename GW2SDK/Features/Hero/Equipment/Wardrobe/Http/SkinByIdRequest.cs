﻿using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Wardrobe.Http;

internal sealed class SkinByIdRequest(int skinId) : IHttpRequest<EquipmentSkin>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/skins")
    {
        AcceptEncoding = "gzip"
    };

    public int SkinId { get; } = skinId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(EquipmentSkin Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", SkinId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetEquipmentSkin(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
