using System.Globalization;
using GuildWars2.Http;
using GuildWars2.Json;

namespace GuildWars2.Exploration.Http;

internal sealed class FloorsIndexRequest(int continentId) : IHttpRequest<HashSet<int>>
{
    private static readonly HttpRequestMessageTemplate Template =
        new(Get, "v2/continents/:id/floors")
        {
            AcceptEncoding = "gzip",
            Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
        };

    public int ContinentId { get; } = continentId;

    public async Task<(HashSet<int> Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with
                {
                    Path = Template.Path.Replace(
                        ":id",
                        ContinentId.ToString(CultureInfo.InvariantCulture)
                    )
                },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetSet(entry => entry.GetInt32());
        return (value, new MessageContext(response));
    }
}
