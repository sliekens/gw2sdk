using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Amulets;

public class AmuletsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            4, 8,
            9
        ];

        var (actual, context) = await sut.Pvp.GetAmuletsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_icon();
                entry.Has_attributes();
            }
        );
    }
}
