using Xunit;

namespace GW2SDK.Tests.Features.Recipes.Fixtures
{
    [CollectionDefinition(nameof(RecipeDbCollection))]
    public sealed class RecipeDbCollection : ICollectionFixture<RecipeFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
