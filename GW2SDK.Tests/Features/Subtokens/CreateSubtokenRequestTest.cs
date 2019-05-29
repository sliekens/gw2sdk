using GW2SDK.Infrastructure.Subtokens;
using Xunit;

namespace GW2SDK.Tests.Features.Subtokens
{
    public class CreateSubtokenRequestTest
    {
        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithDefaultAccessToken_ShouldHaveNoAuthorizationHeader()
        {
            var sut = new CreateSubtokenRequest.Builder().GetRequest();

            Assert.Null(sut.Headers.Authorization);
        }

        [Fact]
        [Trait("Feature",  "Subtokens")]
        [Trait("Category", "Unit")]
        public void CreateSubtokenRequest_WithAccessTokenNull_ShouldHaveNoAuthorizationHeader()
        {
            var sut = new CreateSubtokenRequest.Builder().GetRequest();

            Assert.Null(sut.Headers.Authorization);
        }
    }
}
