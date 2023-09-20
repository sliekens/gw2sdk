using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

// ReSharper disable once ClassNeverInstantiated.Global
public class AchievementFixture
{
    public AchievementFixture()
    {
        Achievements = FlatFileReader.Read("Data/achievements.json.gz").ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Achievements { get; }
}
