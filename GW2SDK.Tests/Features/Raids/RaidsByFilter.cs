using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Raids;

public class RaidsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "forsaken_thicket",
            "bastion_of_the_penitent",
            "hall_of_chains"
        };

        var (actual, context) = await sut.Raids.GetRaidsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
