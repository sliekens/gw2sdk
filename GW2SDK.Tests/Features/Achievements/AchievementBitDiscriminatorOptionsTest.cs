using System.Linq;
using GW2SDK.Achievements.Impl;
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
        public void It_supports_every_known_type_name()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var expected = _fixture.Db.GetAchievementBitTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void It_can_create_objects_of_each_supported_type()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.CreateInstance(type)));
        }

        [Fact]
        [Trait("Feature",  "Achievements")]
        [Trait("Category", "Integration")]
        public void It_only_creates_objects_that_extend_the_correct_base_type()
        {
            var sut = new AchievementBitDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.CreateInstance(type)));
        }
    }
}
