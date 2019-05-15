using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Tests.Features.Colors.Extensions;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorCategoryFixture : IAsyncLifetime
    {
        public ISet<string> ColorCategories { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();
            var http = new HttpClient
            {
                BaseAddress = configuration.BaseAddress
            };

            // TODO: ideally we should use persistent storage for this
            // LiteDB looks like a good candidate for storage
            var service = new JsonColorService(http);
            ColorCategories = await service.GetAllColorCategories();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
