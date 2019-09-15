using System;
using System.Net.Http;
using GW2SDK.Items.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class GetItemByIdRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var id = 1;

            var sut = new GetItemByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_id()
        {
            var id = 1;

            var sut = new GetItemByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/items?id=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
