using System;
using System.Collections.Generic;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Worlds;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldPopulationTest
    {
        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(WorldPopulation), default(WorldPopulation)));
        }

        [Fact]
        [Trait("Feature",    "Worlds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Enum_ShouldHaveNoMissingMembers()
        {
            var expected = new HashSet<string> { "Low", "Medium", "High", "VeryHigh", "Full" };

            var actual = Enum.GetNames(typeof(WorldPopulation)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
