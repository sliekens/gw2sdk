using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class RarityTest
    {
        public RarityTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Rarity_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetSkinRarities().ToHashSet();

            var actual = Enum.GetNames(typeof(Rarity)).ToHashSet();

            Assert.Superset(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Rarity_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(Rarity), default(Rarity)));
        }
    }
}
