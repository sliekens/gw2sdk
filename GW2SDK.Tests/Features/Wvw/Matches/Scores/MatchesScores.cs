using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Scores;

public class MatchesScores
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Wvw.GetMatchesScores(
                cancellationToken: TestContext.Current.CancellationToken
            );

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
                        Assert.All(
                            skirmish.MapScores,
                            score =>
                            {
                                Assert.True(score.Kind.IsDefined());
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
                    }
                );
            }
        );
    }
}
