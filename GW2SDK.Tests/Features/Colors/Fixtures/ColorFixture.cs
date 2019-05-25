using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Colors;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorFixture : IAsyncLifetime
    {
        public string JsonArrayOfColors { get; private set; }

        public async Task InitializeAsync()
        {
            var http = new HttpFixture();

            var service = new ColorJsonService(http.Http);

            var response = await service.GetAllColors();

            response.EnsureSuccessStatusCode();

            JsonArrayOfColors = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
