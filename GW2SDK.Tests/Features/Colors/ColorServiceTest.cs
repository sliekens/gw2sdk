﻿using System;
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
            public static void Base_rgb_contains_red_green_blue(Color actual) => Assert.Collection(actual.BaseRgb,
                red => Assert.InRange(red,     1, 255),
                green => Assert.InRange(green, 1, 255),
                blue => Assert.InRange(blue,   1, 255));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_colors()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColors();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                color =>
                {
                    ColorFact.Base_rgb_contains_red_green_blue(color);
                });
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_color_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_color_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            const int colorId = 1;

            var actual = await sut.GetColorById(colorId);

            Assert.Equal(colorId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_colors_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetColorsByIds(ids);

            Assert.Collection(actual.Values, first => Assert.Equal(1, first.Id), second => Assert.Equal(2, second.Id), third => Assert.Equal(3, third.Id));
        }

        [Fact]
        [Trait("Feature",  "Colors")]
        [Trait("Category", "Unit")]
        public async Task Color_ids_cannot_be_null()
        {
            await using var services = new Composer();
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
            await using var services = new Composer();
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
        public async Task It_can_get_colors_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ColorService>();

            var actual = await sut.GetColorsByPage(1, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
