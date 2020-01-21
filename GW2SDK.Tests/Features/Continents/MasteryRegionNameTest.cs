using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Continents.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    [Collection(nameof(ContinentDbCollection))]
    public class MasteryRegionNameTest
    {
        public MasteryRegionNameTest(FloorFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly FloorFixture _fixture;

        [Fact]
        [Trait("Feature",    "Continents")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Mastery_region_names_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetMasteryRegionNames().ToHashSet();

            var actual = Enum.GetNames(typeof(MasteryRegionName)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
