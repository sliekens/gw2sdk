using System;
using System.Linq;
using GW2SDK.Enums;
using GW2SDK.Tests.Features.Recipes.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    [Collection(nameof(RecipeDbCollection))]
    public class CraftingDisciplineTest
    {
        public CraftingDisciplineTest(RecipeFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly RecipeFixture _fixture;

        [Fact]
        [Trait("Feature",    "Recipes")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Crafting_discipline_can_be_serialized_from_json()
        {
            var expected = _fixture.Db.GetRecipeDisciplines().ToHashSet();

            var actual = Enum.GetNames(typeof(CraftingDiscipline)).ToHashSet();

            Assert.Equal(expected, actual);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public void Crafting_discipline_has_no_default_member()
        {
            Assert.False(Enum.IsDefined(typeof(CraftingDiscipline), default(CraftingDiscipline)));
        }
    }
}
