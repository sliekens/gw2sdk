using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinRestrictionTest
    {
        public SkinRestrictionTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Skin_restriction_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetSkinRestrictions().ToHashSet();

            var actual = Enum.GetNames(typeof(SkinRestriction)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void Skin_restriction_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(SkinRestriction), default(SkinRestriction)));
        }
    }
}
