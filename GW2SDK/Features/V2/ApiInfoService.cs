using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.V2.Http;
using GW2SDK.V2.Json;

using JetBrains.Annotations;

namespace GW2SDK.V2;

[PublicAPI]
public sealed class ApiInfoService
{
    private readonly HttpClient http;

    public ApiInfoService(HttpClient http)
    {
        this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<IReplica<ApiInfo>> GetApiInfo(
        MissingMemberBehavior missingMemberBehavior = default,
        CancellationToken cancellationToken = default
    )
    {
        ApiInfoRequest request = new();
        return await http.GetResource(request,
                json => ApiInfoReader.Read(json.RootElement, missingMemberBehavior),
                cancellationToken)
            .ConfigureAwait(false);
    }
}