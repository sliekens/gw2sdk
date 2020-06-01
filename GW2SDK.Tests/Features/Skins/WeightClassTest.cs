using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class WeightClassTest
    {
        public WeightClassTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Weight_class_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetArmorWeightClasses().ToHashSet();

            var actual = Enum.GetNames(typeof(WeightClass)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Weight_class_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(WeightClass), default(WeightClass)));
        }
    }
}
