using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchesScores
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetMatchesScores();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.All(
                    entry.Skirmishes,
                    skirmish =>
                    {
                        Assert.True(skirmish.Id > 0);
                        Assert.True(skirmish.Scores.Blue > 0);
                        Assert.True(skirmish.Scores.Green > 0);
                        Assert.True(skirmish.Scores.Red > 0);
                        Assert.All(
                            skirmish.MapScores,
                            score =>
                            {
                                Assert.True(score.Kind.IsDefined());
                                Assert.True(score.Scores.Blue > 0);
                                Assert.True(score.Scores.Green > 0);
                                Assert.True(score.Scores.Red > 0);
                            }
                        );
                    }
                );
                Assert.All(
                    entry.Maps,
                    map =>
                    {
                        Assert.True(map.Id > 0);
                        Assert.True(map.Kind.IsDefined());
                        Assert.True(map.Scores.Blue > 0);
                        Assert.True(map.Scores.Green > 0);
                        Assert.True(map.Scores.Red > 0);
                    }
                );
            }
        );
    }
}
