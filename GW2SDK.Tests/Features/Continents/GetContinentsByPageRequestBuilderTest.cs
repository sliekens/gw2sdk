using System;
using System.Net.Http;
using GW2SDK.Continents.Impl;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    public class GetContinentsByPageRequestBuilderTest
    {
        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void Method_is_GET()
        {
            var sut = new GetContinentsByPageRequest.Builder(0);

            var actual = sut.GetRequest();

            Assert.Equal(HttpMethod.Get, actual.Method);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page()
        {
            var sut = new GetContinentsByPageRequest.Builder(1);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/continents?page=1", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Unit")]
        public void RequestUri_contains_specified_page_size()
        {
            var sut = new GetContinentsByPageRequest.Builder(1, 200);

            var actual = sut.GetRequest();

            var expected = new Uri("/v2/continents?page=1&page_size=200", UriKind.Relative);

            Assert.Equal(expected, actual.RequestUri);
        }
    }
}
