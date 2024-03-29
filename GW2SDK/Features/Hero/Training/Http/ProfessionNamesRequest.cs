﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training.Http;

internal sealed class ProfessionNamesRequest : IHttpRequest<HashSet<Extensible<ProfessionName>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/professions")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public async Task<(HashSet<Extensible<ProfessionName>> Value, MessageContext Context)>
        SendAsync(HttpClient httpClient, CancellationToken cancellationToken)
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
        var value = json.RootElement.GetSet(entry => entry.GetEnum<ProfessionName>());
        return (value, new MessageContext(response));
    }
}
