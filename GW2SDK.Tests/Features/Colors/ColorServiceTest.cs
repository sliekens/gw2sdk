using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
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
        [Trait("Category", "Integration")]
        public async Task GetColorIds_ShouldNotReturnEmptyCollection()
        {
            var sut = new ColorService(_http.Http);

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorIds(settings);

            Assert.NotEmpty(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldNotReturnNull()
        {
            var sut = new ColorService(_http.Http);

            const int dyeRemoverId = 1;

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorById(dyeRemoverId, settings);

            Assert.NotNull(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsById_ShouldReturnExpectedRange()
        {
            var sut = new ColorService(_http.Http);

            var ids = Enumerable.Range(1, 5).ToList();

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorsById(ids, settings);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsPage_ShouldReturnExpectedLimit()
        {
            var sut = new ColorService(_http.Http);

            var limit = 50;

            var settings = new JsonSerializerSettingsBuilder()
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetColorsPage(0, limit, settings);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
