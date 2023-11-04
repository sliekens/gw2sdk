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

        var actual = await sut.Raids.GetRaidsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
