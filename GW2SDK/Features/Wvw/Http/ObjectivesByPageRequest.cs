﻿using GuildWars2.Http;
using GuildWars2.Json;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Wvw.Http;

internal sealed class ObjectivesByPageRequest : IHttpRequest<HashSet<Objective>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/wvw/objectives") { AcceptEncoding = "gzip" };

    public ObjectivesByPageRequest(int pageIndex)
    {
        PageIndex = pageIndex;
    }

    public int PageIndex { get; }

    public int? PageSize { get; init; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(HashSet<Objective> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new() { { "page", PageIndex } };
        if (PageSize.HasValue)
        {
            search.Add("page_size", PageSize.Value);
        }

        search.Add("v", SchemaVersion.Recommended);
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = search,
                    AcceptLanguage = Language?.Alpha2Code
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken).ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetObjective(MissingMemberBehavior));
        return (value, new MessageContext(response));
    }
}
