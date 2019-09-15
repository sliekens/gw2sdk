using System;
using System.Threading.Tasks;
using GW2SDK.Colors;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest
    {
        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Get_all_colors()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColors();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Get_all_color_ids()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Get_a_color_by_id()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            const int colorId = 1;

            var actual = await sut.GetColorById(colorId);

            Assert.Equal(colorId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Get_colors_by_id()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetColorsByIds(ids);

            Assert.Collection(actual, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task Color_ids_cannot_be_null()
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
        public async Task Color_ids_cannot_be_empty()
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
        public async Task Get_colors_by_page()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetColorsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            var services = new Container();
            var sut = services.Resolve<ColorService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetColorsByPage(1, -3));
        }
    }
}
