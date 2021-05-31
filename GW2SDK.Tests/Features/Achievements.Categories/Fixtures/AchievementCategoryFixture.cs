using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Categories.Fixtures
{
    public class AchievementCategoryFixture
    {
        public AchievementCategoryFixture()
        {
            var reader = new FlatFileReader();
            AchievementCategories = reader.Read("Data/achievementCategories.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> AchievementCategories { get; }
    }
}
