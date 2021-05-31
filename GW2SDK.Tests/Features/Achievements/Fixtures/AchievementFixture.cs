using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Fixtures
{
    public class AchievementFixture
    {
        public AchievementFixture()
        {
            var reader = new FlatFileReader();
            Achievements = reader.Read("Data/achievements.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Achievements { get; }
    }
}
