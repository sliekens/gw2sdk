using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;
using static System.Net.Http.HttpMethod;

namespace GW2SDK.Meta;

[PublicAPI]
public sealed class ApiVersionRequest : IHttpRequest<IReplica<ApiVersion>>
{
    private static readonly HttpRequestMessageTemplate Template = new(Get, "/:version.json")
    {
        AcceptEncoding = "gzip"
    };

    public ApiVersionRequest(string version)
    {
        Version = version;
    }

    public string Version { get; }

    public MissingMemberBehavior MissingMemberBehavior { get; init; }

    public async Task<IReplica<ApiVersion>> SendAsync(
        HttpClient httpClient,
        CancellationToken cancellationToken
    )
    {
        QueryBuilder search = new();
        var request = Template with
        {
            Path = Template.Path.Replace(":version", Version),
            Arguments = search
        };

        using var response = await httpClient.SendAsync(
                request.Compile(),
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken
                )
            .ConfigureAwait(false);

        await response.EnsureResult(cancellationToken).ConfigureAwait(false);

        using var json = await response.Content.ReadAsJsonAsync(cancellationToken)
            .ConfigureAwait(false);

        var value = json.RootElement.GetApiVersion(MissingMemberBehavior);
        return new Replica<ApiVersion>(
            response.Headers.Date.GetValueOrDefault(),
            value,
            response.Content.Headers.Expires,
            response.Content.Headers.LastModified
            );
    }
}
