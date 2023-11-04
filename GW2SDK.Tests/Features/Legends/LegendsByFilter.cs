using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Legends;

public class LegendsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "Legend1",
            "Legend3",
            "Legend5"
        };

        var actual = await sut.Legends.GetLegendsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_code();
            }
        );
    }
}
