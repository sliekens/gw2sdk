﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Daily.Http;

internal sealed class DailyCraftingRequest : IHttpRequest<HashSet<string>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/dailycrafting")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public async Task<(HashSet<string> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetStringRequired());
        return (value, new MessageContext(response));
    }
}
