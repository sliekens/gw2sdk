using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class MasteryRegionNameTest
    {
        public MasteryRegionNameTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementFixture _fixture;

        [Fact]
        [Trait("Feature",    "Achievements")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Mastery_region_names_can_be_created_from_json()
        {
            var expected = _fixture.Db.GetMasteryRegionNames().ToHashSet();

            var actual = Enum.GetNames(typeof(MasteryRegionName)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
