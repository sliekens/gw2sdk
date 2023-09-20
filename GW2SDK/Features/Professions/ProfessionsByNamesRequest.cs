using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public sealed class ProfessionsByNamesRequest : IHttpRequest<Replica<HashSet<Profession>>>
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

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

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
                        { "ids", ProfessionNames.Select(name => name.ToString()) },
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
        return new Replica<HashSet<Profession>>
        {
            Value =
                json.RootElement.GetSet(entry => entry.GetProfession(MissingMemberBehavior)),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
