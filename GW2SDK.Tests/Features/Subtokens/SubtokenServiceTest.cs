using System.Threading.Tasks;
using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest : IClassFixture<HttpFixture>
    {
        public SubtokenServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        public async Task CreateSubtoken_ShouldReturnCreatedSubtoken()
        {
            var sut = new SubtokenService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.CreateSubtoken(settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }
    }
}
