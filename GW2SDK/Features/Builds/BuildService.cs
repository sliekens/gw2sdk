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
        private readonly IBuildReader buildReader;
        private readonly HttpClient http;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public BuildService(HttpClient http, IBuildReader buildReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.buildReader = buildReader ?? throw new ArgumentNullException(nameof(buildReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<Build>> GetBuild()
        {
            var request = new BuildRequest();
            return await http.GetResource(request, json => buildReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}
