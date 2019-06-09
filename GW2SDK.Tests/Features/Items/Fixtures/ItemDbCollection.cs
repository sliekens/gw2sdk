using Xunit;

namespace GW2SDK.Tests.Features.Items.Fixtures
{
    [CollectionDefinition(nameof(ItemDbCollection))]
    public sealed class ItemDbCollection : ICollectionFixture<ItemFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
