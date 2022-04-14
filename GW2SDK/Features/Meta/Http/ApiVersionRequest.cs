using System.Net.Http;

using GW2SDK.Http;

using JetBrains.Annotations;

using static System.Net.Http.HttpMethod;

namespace GW2SDK.Meta.Http;

[PublicAPI]
public sealed class ApiVersionRequest
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

    public static implicit operator HttpRequestMessage(ApiVersionRequest r)
    {
        var request = Template with
        {
            Path = Template.Path.Replace(":version", r.Version)
        };
        return request.Compile();
    }
}