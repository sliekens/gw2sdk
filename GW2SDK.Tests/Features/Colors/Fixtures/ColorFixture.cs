using System.Threading.Tasks;
using GW2SDK.Infrastructure.Colors;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture : IAsyncLifetime
    {
        public string JsonArrayOfColors { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            using (var request = new GetAllColorsRequest())
            using (var response = await http.Http.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                JsonArrayOfColors = await response.Content.ReadAsStringAsync();
            }
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
