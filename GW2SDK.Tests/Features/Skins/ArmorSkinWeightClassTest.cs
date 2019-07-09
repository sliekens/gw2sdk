using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class ArmorSkinWeightClassTest
    {
        public ArmorSkinWeightClassTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(WeightClass), default(WeightClass)));
        }

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Enum_ShouldHaveNoMissingMembers()
        {
            var expected = _fixture.Db.GetArmorWeightClasses().ToHashSet();

            var actual = Enum.GetNames(typeof(WeightClass)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}