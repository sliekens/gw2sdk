using System.Linq;
using GW2SDK.Continents.Impl;
using GW2SDK.Tests.Features.Continents.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Continents
{
    [Collection(nameof(ContinentDbCollection))]
    public class PointOfInterestDiscriminatorOptionsTest
    {
        public PointOfInterestDiscriminatorOptionsTest(FloorFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly FloorFixture _fixture;

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void It_supports_every_known_type_name()
        {
            var sut = new PointOfInterestDiscriminatorOptions();

            var expected = _fixture.Db.GetPointOfInterestTypeNames();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void It_can_create_objects_of_each_supported_type()
        {
            var sut = new PointOfInterestDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.CreateInstance(type)));
        }

        [Fact]
        [Trait("Feature",  "Continents")]
        [Trait("Category", "Integration")]
        public void It_only_creates_objects_that_extend_the_correct_base_type()
        {
            var sut = new PointOfInterestDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.CreateInstance(type)));
        }
    }
}
