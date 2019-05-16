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
            var service = new JsonColorService(new HttpClient().WithBaseAddress(configuration.BaseAddress));

            JsonArrayOfColors = await service.GetAllColors();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
