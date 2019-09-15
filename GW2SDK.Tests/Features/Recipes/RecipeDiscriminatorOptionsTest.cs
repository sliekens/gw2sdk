using System.Linq;
using GW2SDK.Recipes.Impl;
using GW2SDK.Tests.Features.Recipes.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    [Collection(nameof(RecipeDbCollection))]
    public class RecipeDiscriminatorOptionsTest
    {
        public RecipeDiscriminatorOptionsTest(RecipeFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly RecipeFixture _fixture;

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void It_supports_every_known_type_name()
        {
            var sut = new RecipeDiscriminatorOptions();

            var expected = _fixture.Db.GetRecipeTypeNames().ToHashSet();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.TypeName).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void It_can_create_objects_of_each_supported_type()
        {
            var sut = new RecipeDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsType(type, sut.Create(type)));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void It_only_creates_objects_that_extend_the_correct_base_type()
        {
            var sut = new RecipeDiscriminatorOptions();

            var actual = sut.GetDiscriminatedTypes().Select(x => x.Type).ToList();

            Assert.All(actual, type => Assert.IsAssignableFrom(sut.BaseType, sut.Create(type)));
        }
    }
}
