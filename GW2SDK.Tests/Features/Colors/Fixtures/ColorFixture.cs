using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture : IAsyncLifetime
    {
        public string JsonArrayOfColors { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            var http = new HttpClient()
                .WithBaseAddress(configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var service = new JsonColorService(http);

            JsonArrayOfColors = await service.GetAllColors();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
