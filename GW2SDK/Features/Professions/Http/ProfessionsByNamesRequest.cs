﻿using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Professions.Http;

internal sealed class ProfessionsByNamesRequest : IHttpRequest<Replica<HashSet<Profession>>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/professions")
    {
        AcceptEncoding = "gzip"
    };

    public ProfessionsByNamesRequest(IReadOnlyCollection<ProfessionName> professionNames)
    {
        Check.Collection(professionNames);
        ProfessionNames = professionNames;
    }

    public IReadOnlyCollection<ProfessionName> ProfessionNames { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<HashSet<Profession>>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        {
                            "ids", ProfessionNames.Select(
#if NET
                                id => Enum.GetName(id)!
#else
                                id => Enum.GetName(typeof(ProfessionName), id)!
#endif
                            )
                        },
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
        var value = json.RootElement.GetSet(entry => entry.GetProfession(MissingMemberBehavior));
        return new Replica<HashSet<Profession>>
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
