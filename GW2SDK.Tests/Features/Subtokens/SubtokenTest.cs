using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Subtokens.Fixtures;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenTest : IClassFixture<ConfigurationFixture>, IClassFixture<SubtokenFixture>
    {
        public SubtokenTest(ITestOutputHelper output, SubtokenFixture services, ConfigurationFixture configuration)
        {
            _output = output;
            _services = services;
            _configuration = configuration;
        }

        private readonly ITestOutputHelper _output;

        private readonly ConfigurationFixture _configuration;

        private readonly SubtokenFixture _services;

        [Fact]
        public async Task After_CreateSubtoken_Then_GetTokenInfo_ShouldBeSubtokenInfo()
        {
            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            _services.Http.UseAccessToken(_configuration.ApiKeyFull);

            var createdSubtoken = await _services.SubtokenService.CreateSubtoken(settings);

            _services.Http.UseAccessToken(createdSubtoken.Subtoken);

            var tokenInfo = await _services.TokenInfoService.GetTokenInfo(settings);

            Assert.IsType<SubtokenInfo>(tokenInfo);
        }
    }
}
