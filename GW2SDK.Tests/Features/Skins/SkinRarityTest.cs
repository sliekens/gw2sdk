using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinRarityTest
    {
        public SkinRarityTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(Rarity), default(Rarity)));
        }

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Enum_ShouldHaveNoMissingMembers()
        {
            var expected = _fixture.Db.GetSkinRarities().ToHashSet();

            var actual = Enum.GetNames(typeof(Rarity)).ToHashSet();

            Assert.Superset(expected, actual);
        }
    }
}
