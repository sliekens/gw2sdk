using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Tests.Features.Colors.Extensions;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    public class ColorCategoryFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpClient _http;

        public ColorCategoryFixture()
        {
            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            var handler = new PolicyHttpMessageHandler(policy)
            {
                InnerHandler = new SocketsHttpHandler()
            };
            _http = new HttpClient(handler, true)
            {
                BaseAddress = _configuration.BaseAddress
            };
        }

        public ISet<string> ColorCategories { get; private set; }

        public async Task InitializeAsync()
        {
            _http.UseLatestSchemaVersion();

            // TODO: ideally we should use persistent storage for this
            // LiteDB looks like a good candidate for storage
            var service = new JsonColorService(_http);
            ColorCategories = await service.GetAllColorCategories();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
