using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummariesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            15, 17,
            18
        ];

        var (actual, context) = await sut.Exploration.GetMapSummariesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
