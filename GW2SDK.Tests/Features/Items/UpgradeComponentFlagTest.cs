using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class UpgradeComponentFlagTest
    {
        public UpgradeComponentFlagTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Upgrade_component_flag_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetUpgradeComponentFlags().ToHashSet();

            var actual = Enum.GetNames(typeof(UpgradeComponentFlag)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
