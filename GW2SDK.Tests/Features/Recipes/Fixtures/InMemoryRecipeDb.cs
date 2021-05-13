using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    public class InMemoryRecipeDb
    {
        public InMemoryRecipeDb(IEnumerable<string> objects)
        {
            Recipes = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Recipes { get; }
    }
}
