using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class Titles
{
    [Fact]
    public async Task Titles_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Achievements.GetTitles();

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id >= 1);
                Assert.NotEmpty(entry.Name);
                if (entry.AchievementPointsRequired.HasValue)
                {
                    Assert.InRange(entry.AchievementPointsRequired.Value, 1, 100000);
                }
                else
                {
                    Assert.NotEmpty(entry.Achievements!);
                }
            }
        );
    }
}
