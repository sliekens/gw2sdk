using System.Linq;
using GW2SDK.Infrastructure.Achievements;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementDiscriminatorOptionsTest
    {
        public AchievementDiscriminatorOptionsTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementFixture _fixture;

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void GetDiscriminatedTypes_ShouldReturnEveryTypeName()
        {
            var sut = new AchievementDiscriminatorOptions();

            var expected = _fixture.Db.GetAchievementTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }
    }
}
