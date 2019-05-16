using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Colors;
using GW2SDK.Features.Colors.Infrastructure;
using GW2SDK.Tests.Shared.Extensions;
using GW2SDK.Tests.Shared.Fixtures;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest : IClassFixture<ConfigurationFixture>
    {
        public ColorServiceTest(ConfigurationFixture configuration, ITestOutputHelper output)
        {
            _configuration = configuration;
            _output = output;
        }

        private readonly ConfigurationFixture _configuration;

        private readonly ITestOutputHelper _output;

        private ColorService CreateSut() =>
            new ColorService(new JsonColorService(new HttpClient().WithBaseAddress(_configuration.BaseAddress)));

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorIds_ShouldNotReturnEmptyCollection()
        {
            var sut = CreateSut();

            var actual = await sut.GetColorIds();

            _output.WriteLine("GetColorIds: {0}", actual.ToCsv());

            Assert.NotEmpty(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldNotReturnNull()
        {
            var sut = CreateSut();

            const int dyeRemoverId = 1;

            var actual = await sut.GetColorById(dyeRemoverId);

            Assert.NotNull(actual);
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsById_ShouldReturnExpectedRange()
        {
            var sut = CreateSut();

            var ids = Enumerable.Range(1, 5).ToList();

            var actual = await sut.GetColorsById(ids);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature", "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsPage_ShouldReturnExpectedLimit()
        {
            var sut = CreateSut();

            var limit = 50;

            var actual = await sut.GetColorsPage(0, limit);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
