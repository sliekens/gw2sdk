using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    [CollectionDefinition(nameof(AchievementDbCollection))]
    public class AchievementDbCollection : ICollectionFixture<AchievementFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
