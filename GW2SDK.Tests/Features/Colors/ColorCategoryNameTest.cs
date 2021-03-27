﻿using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Colors.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Colors
{
    [Collection(nameof(ColorDbCollection))]
    public class ColorCategoryNameTest
    {
        public ColorCategoryNameTest(ColorFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ColorFixture _fixture;

        [Fact]
        [Trait("Feature",    "Colors")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Color_category_names_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetColorCategoryNames().ToHashSet();

            var actual = Enum.GetNames(typeof(ColorCategoryName)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}