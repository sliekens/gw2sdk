using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Accounts.Achievements;
using GW2SDK.Tests.Shared.Fixtures;
using Microsoft.Extensions.Http;
using Polly;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Achievements.Fixtures
{
    public class AchievementFixture : IAsyncLifetime
    {
        private readonly ConfigurationFixture _configuration = new ConfigurationFixture();

        private readonly HttpClient _http;

        public AchievementFixture()
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

        public string AchievementJsonArray { get; set; }

        public async Task InitializeAsync()
        {
            var service = new AchievementJsonService(_http);
            _http.UseLatestSchemaVersion();
            _http.UseAccessToken(_configuration.ApiKeyFull);

            var response = await service.GetAchievements();
            response.EnsureSuccessStatusCode();

            AchievementJsonArray = await response.Content.ReadAsStringAsync();
        }

        public Task DisposeAsync()
        {
            _http.Dispose();
            return Task.CompletedTask;
        }
    }
}
