using GuildWars2.Http;

namespace GuildWars2.Metadata.Http;

internal sealed class BuildRequest : IHttpRequest<Build>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "v2/build")
    {
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    
    public async Task<(Build Value, MessageContext Context)> SendAsync(
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
        var value = json.RootElement.GetBuild();
        return (value, new MessageContext(response));
    }
}
