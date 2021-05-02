using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class AchievementFixture
    {
        public AchievementFixture()
        {
            var reader = new FlatFileReader();
            Db = new InMemoryAchievementDb(reader.Read("Data/achievements.json"));
        }

        public InMemoryAchievementDb Db { get; }
    }
}
