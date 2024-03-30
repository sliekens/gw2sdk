using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class Matches
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetMatches();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.True(entry.StartTime > DateTimeOffset.MinValue);
                Assert.True(entry.EndTime > entry.StartTime);
                Assert.All(entry.Skirmishes,
                    skirmish =>
                    {
                        Assert.True(skirmish.Id > 0);
                        Assert.True(skirmish.Scores.Blue > 0);
                        Assert.True(skirmish.Scores.Green > 0);
                        Assert.True(skirmish.Scores.Red > 0);
                        Assert.All(skirmish.MapScores,
                            score =>
                            {
                                Assert.True(score.Kind.IsDefined());
                                Assert.True(score.Scores.Blue > 0);
                                Assert.True(score.Scores.Green > 0);
                                Assert.True(score.Scores.Red > 0);
                            });
                    });
                Assert.All(entry.Maps,
                    map =>
                    {
                        Assert.True(map.Id > 0);
                        Assert.True(map.Kind.IsDefined());
                        Assert.True(map.Scores.Blue > 0);
                        Assert.True(map.Scores.Green > 0);
                        Assert.True(map.Scores.Red > 0);
                        Assert.All(map.Bonuses,
                            bonus =>
                            {
                                Assert.True(bonus.Kind.IsDefined());
                                Assert.True(bonus.Owner.IsDefined());
                            });
                        Assert.All(map.Objectives,
                            objective =>
                            {
                                Assert.NotEmpty(objective.Id);
                                Assert.True(objective.LastFlipped > DateTimeOffset.MinValue);
                                Assert.True(objective.Owner.IsDefined());
                                if (objective is not Spawn and not Mercenary and not Ruins)
                                {
                                    Assert.True(objective.PointsCapture > 0);
                                    Assert.True(objective.PointsTick > 0);
                                }
                            });
                    });
            }
        );
    }
}
