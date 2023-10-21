using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Maps.GetContinentsByPage(0, pageSize);

        Assert.NotNull(actual.PageContext);
        Assert.Equal(pageSize, actual.PageContext.PageSize);
        Assert.Equal(2, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(2, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_dimensions();
                entry.Has_min_zoom();
                entry.Has_max_zoom();
                entry.Has_floors();
            }
        );
    }
}
