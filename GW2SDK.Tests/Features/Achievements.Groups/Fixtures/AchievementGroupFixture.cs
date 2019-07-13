using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    public class AchievementGroupFixture
    {
        public AchievementGroupFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemoryAchievementGroupDb(reader.Read("Data/achievementGroups.json"));
        }

        public InMemoryAchievementGroupDb Db { get; }
    }
}
