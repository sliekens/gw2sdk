using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.V2.Http;
using JetBrains.Annotations;

namespace GW2SDK.V2
{
    [PublicAPI]
    public sealed class ApiInfoService
    {
        private readonly IApiInfoReader _apiInfoReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public ApiInfoService(HttpClient http, IApiInfoReader apiInfoReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _apiInfoReader = apiInfoReader ?? throw new ArgumentNullException(nameof(apiInfoReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<ApiInfo>> GetApiInfo()
        {
            var request = new ApiInfoRequest();
            return await _http.GetResource(request, json => _apiInfoReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
