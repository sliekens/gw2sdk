using Xunit;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    [CollectionDefinition(nameof(AchievementGroupDbCollection))]
    public class AchievementGroupDbCollection : ICollectionFixture<AchievementGroupFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
