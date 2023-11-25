using GuildWars2.Exploration.Continents;
using GuildWars2.Http;

namespace GuildWars2.Exploration.Http;

internal sealed class ContinentByIdRequest : IHttpRequest<Continent>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/continents")
    {
        AcceptEncoding = "gzip"
    };

    public ContinentByIdRequest(int continentId)
    {
        ContinentId = continentId;
    }

    public int ContinentId { get; }

    public Language? Language { get; init; }

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(Continent Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Arguments = new QueryBuilder
                    {
                        { "id", ContinentId },
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
        var value = json.RootElement.GetContinent(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
