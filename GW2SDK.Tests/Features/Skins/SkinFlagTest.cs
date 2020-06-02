using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinFlagTest
    {
        public SkinFlagTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Skin_flag_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetSkinFlags().ToHashSet();

            var actual = Enum.GetNames(typeof(SkinFlag)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Unit")]
        public void Skin_flag_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(SkinFlag), default(SkinFlag)));
        }
    }
}
