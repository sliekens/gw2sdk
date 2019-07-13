using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    public class InMemoryAchievementGroupDb
    {
        public InMemoryAchievementGroupDb(IEnumerable<string> objects)
        {
            AchievementGroups = objects.ToList().AsReadOnly();
        }

        public IReadOnlyList<string> AchievementGroups { get; }
    }
}
