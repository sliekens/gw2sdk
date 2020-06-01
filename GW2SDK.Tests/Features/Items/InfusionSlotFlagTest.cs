using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Items.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class InfusionSlotFlagTest
    {
        public InfusionSlotFlagTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Infusion_slot_flag_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetInfusionSlotFlags()
                .Concat(_fixture.Db.GetUpgradeComponentInfusionUpgradeFlags())
                .OrderBy(name => name)
                .Distinct()
                .ToHashSet();

            var actual = Enum.GetNames(typeof(InfusionSlotFlag)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public void Infusion_slot_flag_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(InfusionSlotFlag), default(InfusionSlotFlag)));
        }
    }
}
