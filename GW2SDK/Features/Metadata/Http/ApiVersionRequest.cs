using GuildWars2.Http;

namespace GuildWars2.Metadata.Http;

internal sealed class ApiVersionRequest(string version) : IHttpRequest<ApiVersion>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/:version.json")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public string Version { get; } = version;

    public required MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<(ApiVersion Value, MessageContext Context)> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        using var response = await httpClient.SendAsync(
                Template with { Path = Template.Path.Replace(":version", Version) },
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
            )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);
        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);
        var value = json.RootElement.GetApiVersion(MissingMemberBehavior);
        return (value, new MessageContext(response));
    }
}
