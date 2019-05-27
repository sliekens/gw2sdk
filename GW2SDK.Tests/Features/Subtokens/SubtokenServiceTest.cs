using System.Threading.Tasks;
using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest
    {
        public SubtokenServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        public async Task CreateSubtoken_ShouldReturnCreatedSubtoken()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }
    }
}
