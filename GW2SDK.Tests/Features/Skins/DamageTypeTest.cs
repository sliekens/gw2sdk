using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class DamageTypeTest
    {
        public DamageTypeTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Damage_type_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetWeaponDamageTypes().ToHashSet();

            var actual = Enum.GetNames(typeof(DamageType)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
