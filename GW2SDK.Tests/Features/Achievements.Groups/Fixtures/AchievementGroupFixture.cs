using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements.Groups.Fixtures
{
    public class AchievementGroupFixture
    {
        public AchievementGroupFixture()
        {
            var reader = new FlatFileReader();
            AchievementGroups = reader.Read("Data/achievementGroups.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> AchievementGroups { get; }
    }
}
