using System;
using System.Net.Http;
using GW2SDK.Worlds.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class GetWorldByIdRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var id = 1;

            var sut = new GetWorldByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_id()
        {
            var id = 1;

            var sut = new GetWorldByIdRequest.Builder(id);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/worlds?id=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
