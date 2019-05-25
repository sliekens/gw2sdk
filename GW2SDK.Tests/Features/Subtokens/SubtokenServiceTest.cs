using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Subtokens;
using GW2SDK.Infrastructure.Subtokens;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class SubtokenServiceTest : IClassFixture<HttpFixture>
    {
        public SubtokenServiceTest(HttpFixture http)
        {
            _http = http;
        }

        private readonly HttpFixture _http;

        private SubtokenService CreateSut()
        {
            var api = new SubtokenJsonService(_http.HttpFullAccess);
            return new SubtokenService(api);
        }

        [Fact]
        public async Task CreateSubtoken_ShouldNotBeNull()
        {
            var sut = CreateSut();

            var actual = await sut.CreateSubtoken();

            Assert.NotNull(actual);
        }
    }
}
