using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Builds.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Builds
{
    [PublicAPI]
    public sealed class BuildService
    {
        private readonly IBuildReader _buildReader;
        private readonly HttpClient _http;
        private readonly MissingMemberBehavior _missingMemberBehavior;

        public BuildService(HttpClient http, IBuildReader buildReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _buildReader = buildReader ?? throw new ArgumentNullException(nameof(buildReader));
            _missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IDataTransfer<Build>> GetBuild()
        {
            var request = new BuildRequest();
            return await _http.GetResource(request, json => _buildReader.Read(json, _missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
