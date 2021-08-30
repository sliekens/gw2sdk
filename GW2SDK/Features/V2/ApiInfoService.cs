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
        private readonly IApiInfoReader apiInfoReader;
        private readonly HttpClient http;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public ApiInfoService(HttpClient http, IApiInfoReader apiInfoReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.apiInfoReader = apiInfoReader ?? throw new ArgumentNullException(nameof(apiInfoReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<ApiInfo>> GetApiInfo()
        {
            var request = new ApiInfoRequest();
            return await http.GetResource(request, json => apiInfoReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
