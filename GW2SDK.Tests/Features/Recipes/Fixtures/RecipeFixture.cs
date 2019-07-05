using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    public class RecipeFixture
    {
        public RecipeFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var item in reader.Read("Data/recipes.json"))
            {
                Db.AddRecipe(item);
            }
        }

        public InMemoryRecipeDb Db { get; } = new InMemoryRecipeDb();
    }
}
