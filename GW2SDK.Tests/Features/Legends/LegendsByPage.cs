using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Legends;

public class LegendsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Legends.GetLegendsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.PageContext.PageSize);
        Assert.Equal(pageSize, actual.Value.Count);
        Assert.Equal(pageSize, actual.ResultContext.ResultCount);
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
