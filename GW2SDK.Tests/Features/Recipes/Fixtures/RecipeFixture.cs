using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    public class RecipeFixture
    {
        public RecipeFixture()
        {
            var reader = new FlatFileReader();

            Db = new InMemoryRecipeDb(reader.Read("Data/recipes.json"));
        }

        public InMemoryRecipeDb Db { get; }
    }
}
