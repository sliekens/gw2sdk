using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Colors;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    public class ColorServiceTest
    {
        private static class ColorFact
        {
            public static void Base_rgb_contains_red_green_blue(Dye actual)
            {
                Assert.False(actual.BaseRgb.IsEmpty);
            }
        }

        [Fact]
        public async Task It_can_get_all_colors()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColors();

            Assert.Equal(actual.Context.ResultTotal, actual.Count);
            Assert.All(actual,
                color =>
                {
                    ColorFact.Base_rgb_contains_red_green_blue(color);
                });
        }

        [Fact]
        public async Task It_can_get_all_color_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Count);
        }

        [Fact]
        public async Task It_can_get_a_color_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            const int colorId = 1;

            var actual = await sut.GetColorById(colorId);

            Assert.Equal(colorId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_colors_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var ids = new HashSet<int>
            {
                1,
                2,
                3
            };

            var actual = await sut.GetColorsByIds(ids);

            Assert.Collection(actual,
                first => Assert.Equal(1, first.Id),
                second => Assert.Equal(2, second.Id),
                third => Assert.Equal(3, third.Id));
        }

        [Fact]
        public async Task It_can_get_colors_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsByPage(0, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
