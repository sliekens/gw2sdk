using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Common;
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
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessToken_ShouldReturnCreatedSubtoken()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.CreateSubtoken(ConfigurationManager.Instance.ApiKeyFull, settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenInDefaultRequestHeaders_ShouldReturnCreatedSubtoken()
        {
            var http = HttpClientFactory.CreateDefault();
            http.UseAccessToken(ConfigurationManager.Instance.ApiKeyFull);

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.CreateSubtoken(null, settings);

            Assert.IsType<CreatedSubtoken>(actual);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Integration")]
        public async Task CreateSubtoken_WithAccessTokenNull_ShouldThrowUnauthorizedOperationException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new SubtokenService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            await Assert.ThrowsAsync<UnauthorizedOperationException>(async () =>
            {
                // Next statement should throw because argument is null and HttpClient.DefaultRequestHeaders is not configured
                _ = await sut.CreateSubtoken(null, settings);
            });
        }
    }
}
