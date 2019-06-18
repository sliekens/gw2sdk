using System.Linq;
using GW2SDK.Infrastructure.Achievements;
using GW2SDK.Tests.Features.Achievements.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Achievements
{
    [Collection(nameof(AchievementDbCollection))]
    public class AchievementBitDiscriminatorOptionsTest
    {
        public AchievementBitDiscriminatorOptionsTest(AchievementFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AchievementFixture _fixture;

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void GetDiscriminatedTypes_ShouldReturnEveryTypeName()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var expected = _fixture.Db.GetAchievementBitTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Create_ShouldReturnExpectedObject()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.Create(type)));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void Create_ShouldReturnObjectAssignableFromBaseType()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.Create(type)));
        }
    }
}
