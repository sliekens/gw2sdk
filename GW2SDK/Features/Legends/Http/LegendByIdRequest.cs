﻿using GuildWars2.Http;

namespace GuildWars2.Legends.Http;

internal sealed class LegendByIdRequest : IHttpRequest<Legend>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/legends") { AcceptEncoding = "gzip" };

    public LegendByIdRequest(string legendId)
    {
        LegendId = legendId;
    }

    public string LegendId { get; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Legend Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", LegendId },
                        { "v", SchemaVersion.Recommended }
                    }
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetLegend(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
