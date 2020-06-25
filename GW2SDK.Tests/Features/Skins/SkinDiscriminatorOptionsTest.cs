using System.Linq;
using GW2SDK.Skins.Impl;
using GW2SDK.Tests.Features.Skins.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinDiscriminatorOptionsTest
    {
        public SkinDiscriminatorOptionsTest(SkinFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly SkinFixture _fixture;

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public void It_supports_every_known_type_name()
        {
            var sut = new SkinDiscriminatorOptions();

            var expected = _fixture.Db.GetSkinTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public void It_can_create_objects_of_each_supported_type()
        {
            var sut = new SkinDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.CreateInstance(type)));
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public void It_only_creates_objects_that_extend_the_correct_base_type()
        {
            var sut = new SkinDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.CreateInstance(type)));
        }
    }
}
