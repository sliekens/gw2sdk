using GW2SDK.Tests.Shared;

namespace GW2SDK.Tests.Features.Achievements.Categories.Fixtures
{
    public class AchievementCategoryFixture
    {
        public AchievementCategoryFixture()
        {
            var reader = new JsonFlatFileReader();

            foreach (var achievement in reader.Read("Data/achievementCategories.json"))
            {
                Db.AddAchievementCategory(achievement);
            }
        }

        public InMemoryAchievementCategoryDb Db { get; } = new InMemoryAchievementCategoryDb();
    }
}
