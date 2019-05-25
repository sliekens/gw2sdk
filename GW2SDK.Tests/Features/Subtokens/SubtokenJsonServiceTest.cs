using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenJsonServiceTest : IClassFixture<ConfigurationFixture>
    {
        public SubtokenJsonServiceTest(ConfigurationFixture configuration, ITestOutputHelper output)
        {
            _configuration = configuration;
            _output = output;
        }

        private readonly ConfigurationFixture _configuration;

        private readonly ITestOutputHelper _output;

        private SubtokenJsonService CreateSut()
        {
            var http = new HttpClient
            {
                BaseAddress = _configuration.BaseAddress
            };
            http.UseAccessToken(_configuration.ApiKeyFull);
            http.UseLatestSchemaVersion();
            return new SubtokenJsonService(http);
        }

        [Fact]
        public async Task CreateSubtoken_ShouldNotBeEmpty()
        {
            var sut = CreateSut();

            var response = await sut.CreateSubtoken().ConfigureAwait(true);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            _output.WriteLine(json);

            Assert.NotEmpty(json);
        }
    }
}
