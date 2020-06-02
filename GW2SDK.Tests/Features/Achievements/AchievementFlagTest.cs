using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementFlagTest
    {
        public AchievementFlagTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementFixture _fixture;

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void AchievementFlag_can_be_created_from_json()
        {
            var expected = Enum.GetNames(typeof(AchievementFlag)).ToHashSet();

            var actual = _fixture.Db.GetAchievementFlags().ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Unit")]
        public void AchievementFlag_has_no_default_member()
        {
            var actual = Enum.IsDefined(typeof(AchievementFlag), default(AchievementFlag));

            Assert.False(actual);
        }
    }
}
