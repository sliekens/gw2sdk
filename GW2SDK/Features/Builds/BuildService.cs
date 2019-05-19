using System;
using System.Threading.Tasks;
using GW2SDK.Features.Builds.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Builds
{
    public sealed class BuildService
    {
        private readonly IBuildJsonService _api;

        public BuildService([NotNull] IBuildJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<Build> GetBuild([CanBeNull] JsonSerializerSettings settings = null)
        {
            var (json, _) = await _api.GetBuild();
            return JsonConvert.DeserializeObject<Build>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}
