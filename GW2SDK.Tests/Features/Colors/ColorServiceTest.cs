using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Colors;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest
    {
        public ColorServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorIds_ShouldNotReturnEmptyCollection()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetColorIds(settings);

            Assert.NotEmpty(actual);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldNotReturnNull()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            const int dyeRemoverId = 1;

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetColorById(dyeRemoverId, settings);

            Assert.NotNull(actual);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task GetColorsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            await Assert.ThrowsAsync<ArgumentNullException>("colorIds", async () =>
            {
                await sut.GetColorsByIds(null);
            });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task GetColorsByIds_WithIdsEmpty_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            await Assert.ThrowsAsync<ArgumentException>("colorIds", async () =>
            {
                await sut.GetColorsByIds(Enumerable.Empty<int>().ToList());
            });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByIds_ShouldReturnExpectedRange()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            var ids = Enumerable.Range(1, 5).ToList();

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetColorsByIds(ids, settings);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByPage_ShouldReturnExpectedLimit()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ColorService(http);

            var limit = 50;

            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = await sut.GetColorsByPage(0, limit, settings);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
