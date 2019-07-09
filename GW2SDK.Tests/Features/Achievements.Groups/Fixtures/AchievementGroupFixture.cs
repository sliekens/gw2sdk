using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    public class AchievementGroupFixture
    {
        public AchievementGroupFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var achievement in reader.Read("Data/achievementGroups.json"))
            {
                Db.AddAchievementCategory(achievement);
            }
        }

        public InMemoryAchievementGroupDb Db { get; } = new InMemoryAchievementGroupDb();
    }
}
