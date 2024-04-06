using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchesStats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetMatchesStats();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotNull(entry.Kills);
                Assert.NotNull(entry.Deaths);
                Assert.NotEmpty(entry.Maps);
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
