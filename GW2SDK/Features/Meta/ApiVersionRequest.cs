using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Http;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GuildWars2.Meta;

[PublicAPI]
public sealed class ApiVersionRequest : IHttpRequest<Replica<ApiVersion>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/:version.json")
    {
        AcceptEncoding = "gzip",
        Arguments = new QueryBuilder { { "v", SchemaVersion.Recommended } }
    };

    public ApiVersionRequest(string version)
    {
        Version = version;
    }

    public string Version { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<Replica<ApiVersion>> SendAsync(
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
        return new Replica<ApiVersion>
        {
            Value = json.RootElement.GetApiVersion(MissingMemberBehavior),
            ResultContext = response.Headers.GetResultContext(),
            PageContext = response.Headers.GetPageContext(),
            Date = response.Headers.Date.GetValueOrDefault(),
            Expires = response.Content.Headers.Expires,
            LastModified = response.Content.Headers.LastModified
        };
    }
}
