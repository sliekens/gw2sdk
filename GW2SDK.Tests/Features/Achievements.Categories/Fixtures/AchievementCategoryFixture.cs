using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Categories.Fixtures
{
    public class AchievementCategoryFixture
    {
        public AchievementCategoryFixture()
        {
            var reader = new JsonFlatFileReader();
            Db = new InMemoryAchievementCategoryDb(reader.Read("Data/achievementCategories.json"));
        }

        public InMemoryAchievementCategoryDb Db { get; }
    }
}
