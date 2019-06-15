using System;
using System.Net.Http;
using GW2SDK.Infrastructure.Accounts;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class GetAccountRequestTest
    {
        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Unit")]
        public void Method_ShouldBeGet()
        {
            var sut = new GetAccountRequest();

            Assert.Equal(HttpMethod.Get, sut.Method);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void RequestUri_ShouldBeV2Account()
        {
            var sut = new GetAccountRequest();

            var expected = new Uri("/v2/account", UriKind.Relative);

            Assert.Equal(expected, sut.RequestUri);
        }
    }
}
