using Xunit;

namespace GW2SDK.Tests.Features.Colors.Fixtures
{
    [CollectionDefinition(nameof(ColorDbCollection))]
    public class ColorDbCollection : ICollectionFixture<ColorFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
