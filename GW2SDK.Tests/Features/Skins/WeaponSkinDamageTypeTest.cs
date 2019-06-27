using System;
using System.Linq;
using GW2SDK.Features.Common;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class WeaponSkinDamageTypeTest
    {
        public WeaponSkinDamageTypeTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(DamageType), default(DamageType)));
        }

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Enum_ShouldHaveNoMissingMembers()
        {
            var expected = _fixture.Db.GetWeaponDamageTypes().ToHashSet();

            var actual = Enum.GetNames(typeof(DamageType)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}