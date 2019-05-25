using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure.Colors;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest : IClassFixture<HttpFixture>
    {
        public ColorServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "E2E")]
        public async Task GetColorIds_ShouldNotReturnEmptyCollection()
        {
            var sut = new ColorService(_http.Http);

            var actual = await sut.GetColorIds();

            _output.WriteLine("GetColorIds: {0}", actual.ToCsv());

            Assert.NotEmpty(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "E2E")]
        public async Task GetColorById_ShouldNotReturnNull()
        {
            var sut = new ColorService(_http.Http);

            const int dyeRemoverId = 1;

            var actual = await sut.GetColorById(dyeRemoverId);

            Assert.NotNull(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "E2E")]
        public async Task GetColorsById_ShouldReturnExpectedRange()
        {
            var sut = new ColorService(_http.Http);

            var ids = Enumerable.Range(1, 5).ToList();

            var actual = await sut.GetColorsById(ids);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "E2E")]
        public async Task GetColorsPage_ShouldReturnExpectedLimit()
        {
            var sut = new ColorService(_http.Http);

            var limit = 50;

            var actual = await sut.GetColorsPage(0, limit);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
