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

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Create_ShouldReturnExpectedObject()
        {
            var sut = new AchievementRewardDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.Create(type)));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Create_ShouldReturnObjectAssignableFromBaseType()
        {
            var sut = new AchievementRewardDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.Create(type)));
        }
    }
}
