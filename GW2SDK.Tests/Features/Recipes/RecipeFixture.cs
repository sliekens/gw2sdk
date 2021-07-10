using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Recipes
{
    public class RecipeFixture
    {
        public RecipeFixture()
        {
            var reader = new FlatFileReader();

            Recipes = reader.Read("Data/recipes.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Recipes { get; }
    }
}
