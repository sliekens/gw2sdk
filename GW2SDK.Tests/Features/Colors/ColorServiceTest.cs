using System;
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
        public async Task GetColors_ShouldReturnAllColors()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColors();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorById_ShouldReturnThatColor()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

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
        public async Task GetColorsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>("colorIds",
                async () =>
                {
                    await sut.GetColorsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByIds_ShouldReturnThoseColors()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetColorsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetColorsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetColorsByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task GetColorsByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
