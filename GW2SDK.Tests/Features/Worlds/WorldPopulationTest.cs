using System;
using System.Collections.Generic;
using System.Linq;
using GW2SDK.Enums;
using Xunit;

namespace GW2SDK.Tests.Features.Worlds
{
    public class WorldPopulationTest
    {
        [Fact]
        [Trait("Feature",    "Worlds")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void World_population_can_be_serialized_from_json()
        {
            var expected = new HashSet<string> { "Low", "Medium", "High", "VeryHigh", "Full" };

            var actual = Enum.GetNames(typeof(WorldPopulation)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Worlds")]
        [Trait("Category", "Unit")]
        public void World_population_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(WorldPopulation), default(WorldPopulation)));
        }
    }
}
