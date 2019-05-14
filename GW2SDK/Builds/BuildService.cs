using System.Threading.Tasks;
using GW2SDK.Builds.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Builds
{
    public class BuildService
    {
        private readonly IJsonBuildService _api;

        public BuildService(IJsonBuildService api)
        {
            _api = api;
        }

        public async Task<Build> GetBuildAsync()
        {
            var json = await _api.GetBuildAsync();
            return JsonConvert.DeserializeObject<Build>(json);
        }
    }
}