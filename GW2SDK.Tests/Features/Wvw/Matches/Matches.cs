using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class Matches
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Wvw.GetMatches();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_start_time();
                entry.Has_end_time();
            }
        );
    }
}
