using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Achievements
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AchievementFixture
    {
        public AchievementFixture()
        {
            Achievements = FlatFileReader.Read("Data/achievements.json.gz")
                .ToList()
                .AsReadOnly();
        }

        public IReadOnlyCollection<string> Achievements { get; }
    }
}
