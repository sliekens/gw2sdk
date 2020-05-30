using System.Collections.Generic;
using System.Linq;

namespace GW2SDK.Tests.Features.Accounts.Achievements.Fixtures
{
    public class InMemoryAccountAchievementsDb
    {
        public InMemoryAccountAchievementsDb(IEnumerable<string> objects)
        {
            AccountAchievements = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> AccountAchievements { get; }
    }
}
