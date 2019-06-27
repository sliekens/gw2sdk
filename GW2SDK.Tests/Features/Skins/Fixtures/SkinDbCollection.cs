using Xunit;

namespace GW2SDK.Tests.Features.Skins.Fixtures
{
    [CollectionDefinition(nameof(SkinDbCollection))]
    public sealed class SkinDbCollection : ICollectionFixture<SkinFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
