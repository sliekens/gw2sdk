using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class AchievementFixture
    {
        public AchievementFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var achievement in reader.Read("Data/achievements.json"))
            {
                Db.AddAchievement(achievement);
            }
        }

        public InMemoryAchievementDb Db { get; } = new InMemoryAchievementDb();
    }
}
