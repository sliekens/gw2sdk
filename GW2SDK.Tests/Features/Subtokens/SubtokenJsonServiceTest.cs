using System.Threading.Tasks;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenJsonServiceTest : IClassFixture<HttpFixture>
    {
        public SubtokenJsonServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        private SubtokenJsonService CreateSut() => new SubtokenJsonService(_http.HttpFullAccess);

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
