﻿using GuildWars2.Http;

namespace GuildWars2.Hero.Equipment.Gliders.Http;

internal sealed class GliderByIdRequest(int gliderId) : IHttpRequest<Glider>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/gliders") { AcceptEncoding = "gzip" };

    public int GliderId { get; } = gliderId;

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Glider Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", GliderId },
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
        var value = json.RootElement.GetGlider(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}