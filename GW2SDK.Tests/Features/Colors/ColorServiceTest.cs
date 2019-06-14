using System;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Colors;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorIds_ShouldReturnAllColorIds()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsIndex();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldReturnRequestedColor()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            // Randomly chosen
            const int colorId = 1;

            var actual = await sut.GetColorById(colorId);

            Assert.Equal(colorId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task GetColorsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentNullException>("colorIds",
                async () =>
                {
                    await sut.GetColorsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task GetColorsByIds_WithIdsEmpty_ShouldThrowArgumentNullException()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>("colorIds",
                async () =>
                {
                    await sut.GetColorsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByIds_ShouldReturnExpectedRange()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var ids = Enumerable.Range(1, 5).ToList();

            var actual = await sut.GetColorsByIds(ids);

            Assert.Equal(ids, actual.Select(color => color.Id));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColors_ShouldReturnAllColors()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColors();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultTotal);
            Assert.Equal(actual.Count, actual.ResultCount);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByPage_ShouldReturnExpectedLimit()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var limit = 50;

            var actual = await sut.GetColorsByPage(0, limit);

            Assert.InRange(actual.Count, 0, limit);
        }
    }
}
