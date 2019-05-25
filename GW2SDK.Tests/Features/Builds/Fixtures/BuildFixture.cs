using System.Threading.Tasks;
using GW2SDK.Infrastructure.Builds;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Builds.Fixtures
{
    public class BuildFixture : IAsyncLifetime
    {
        public string JsonBuildObject { get; set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            using (var request = new GetBuildRequest())
            using (var response = await http.Http.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                JsonBuildObject = await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
