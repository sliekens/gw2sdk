﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Races;

[PublicAPI]
public sealed class RacesByIdsRequest : IHttpRequest<Replica<HashSet<Race>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(HttpMethod.Get, "v2/races")
    {
        AcceptEncoding = "gzip"
    };

    public RacesByIdsRequest(IReadOnlyCollection<RaceName> raceIds)
    {
        Check.Collection(raceIds, nameof(raceIds));
        RaceIds = raceIds;
    }

    public IReadOnlyCollection<RaceName> RaceIds { get; }

    public Language? Language { get; init; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Race>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "ids", RaceIds.Select(id => id.ToString()) },
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
        return new Replica<HashSet<Race>>
        {
            Value = json.RootElement.GetSet(entry => entry.GetRace(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
