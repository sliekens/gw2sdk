using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementCategories
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Achievements.GetAchievementCategories();

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);
                Assert.NotNull(entry.Description);
                Assert.True(entry.Order >= 0);
                Assert.NotEmpty(entry.IconHref);
                Assert.NotNull(entry.Achievements);

                Assert.All(
                    entry.Achievements,
                    achievement =>
                    {
                        Assert.True(achievement.Id > 0);
                        Assert.Empty(achievement.Flags.Other);
                        if (achievement.Level is not null)
                        {
                            Assert.InRange(achievement.Level.Min, 1, 80);
                            Assert.InRange(achievement.Level.Max, 1, 80);
                            Assert.True(achievement.Level.Max >= achievement.Level.Min);
                        }
                    }
                );

                if (entry.Tomorrow is not null)
                {
                    Assert.All(
                        entry.Tomorrow,
                        achievement =>
                        {
                            Assert.True(achievement.Id > 0);
                            Assert.Empty(achievement.Flags.Other);
                            if (achievement.Level is not null)
                            {
                                Assert.InRange(achievement.Level.Min, 1, 80);
                                Assert.InRange(achievement.Level.Max, 1, 80);
                                Assert.True(achievement.Level.Max >= achievement.Level.Min);
                            }
                        }
                    );
                }
            }
        );
    }
}
