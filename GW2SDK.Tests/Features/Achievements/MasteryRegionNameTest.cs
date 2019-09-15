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
        public void Mastery_region_names_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetAchievementMasteryRegionNames().ToHashSet();

            var actual = Enum.GetNames(typeof(MasteryRegionName)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void There_is_no_default_mastery_region_name()
        {
            Assert.False(Enum.IsDefined(typeof(MasteryRegionName), default(MasteryRegionName)));
        }
    }
}
