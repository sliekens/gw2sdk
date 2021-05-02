using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class InMemoryAchievementDb
    {
        public InMemoryAchievementDb(IEnumerable<string> objects)
        {
            Achievements = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Achievements { get; }
    }
}
