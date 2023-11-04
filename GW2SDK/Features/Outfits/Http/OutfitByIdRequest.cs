﻿using GuildWars2.Http;

namespace GuildWars2.Outfits.Http;

internal sealed class OutfitByIdRequest : IHttpRequest<Replica<Outfit>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/outfits") { AcceptEncoding = "gzip" };

    public OutfitByIdRequest(int outfitId)
    {
        OutfitId = outfitId;
    }

    public int OutfitId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<Outfit>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", OutfitId },
                        { "v", SchemaVersion.Recommended }
                    },
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetOutfit(MissingMemberBehavior);
        return new Replica<Outfit>
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
