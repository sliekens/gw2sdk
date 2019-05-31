using System;
using System.Linq;
using GW2SDK.Features.Achievements;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class MasteryRegionNameTest
    {
        private readonly AchievementFixture _fixture;

        public MasteryRegionNameTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [Trait("Feature", "Achievements")]
        [Trait("Category", "Unit")]
        public void DefaultMember_ShouldBeUndefined()
        {
            Assert.False(Enum.IsDefined(typeof(MasteryRegionName), default(MasteryRegionName)));
        }

        [Fact]
        [Trait("Feature", "Achievements")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void AllMembers_ShouldNotHaveMissingMembers()
        {
            var expected = _fixture.Db.GetAchievementMasteryRegionNames().ToHashSet();

            var actual = Enum.GetNames(typeof(MasteryRegionName)).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
