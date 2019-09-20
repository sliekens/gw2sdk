using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices.Fixtures
{
    [CollectionDefinition(nameof(ItemPriceDbCollection))]
    public sealed class ItemPriceDbCollection : ICollectionFixture<ItemPriceFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
