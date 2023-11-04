using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchesStatsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "1-1",
            "1-2",
            "1-3"
        };

        var actual = await sut.Wvw.GetMatchesStatsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_kills();
                entry.Has_deaths();
                entry.Has_maps();
            }
        );
    }
}
