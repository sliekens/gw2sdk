using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Achievements.Categories.Fixtures
{
    public class InMemoryAchievementCategoryDb
    {
        public InMemoryAchievementCategoryDb(IEnumerable<string> objects)
        {
            AchievementCategories = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> AchievementCategories { get; }
    }
}
