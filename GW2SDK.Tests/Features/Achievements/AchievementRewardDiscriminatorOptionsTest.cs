using System.Linq;
using GW2SDK.Infrastructure.Achievements;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementRewardDiscriminatorOptionsTest
    {
        public AchievementRewardDiscriminatorOptionsTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementFixture _fixture;

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void GetDiscriminatedTypes_ShouldReturnEveryTypeName()
        {
            var sut = new AchievementRewardDiscriminatorOptions();

            var expected = _fixture.Db.GetAchievementRewardTypeNames();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
