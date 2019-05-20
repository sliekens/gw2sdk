﻿using System;
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
        private readonly HttpClient _http;

        public ColorFixture()
        {
            var policy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(3));
            var handler = new PolicyHttpMessageHandler(policy)
            {
                InnerHandler = new SocketsHttpHandler()
            };
            _http = new HttpClient(handler, true);
        }

        public string JsonArrayOfColors { get; private set; }

        public async Task InitializeAsync()
        {
            var configuration = new ConfigurationFixture();

            _http.WithBaseAddress(configuration.BaseAddress)
                .WithLatestSchemaVersion();

            var service = new ColorJsonService(_http);

            var response = await service.GetAllColors();
            response.EnsureSuccessStatusCode();
            JsonArrayOfColors = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync() => Task.CompletedTask;
    }
}
