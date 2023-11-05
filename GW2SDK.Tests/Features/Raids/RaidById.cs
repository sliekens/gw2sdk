using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Raids;

public class RaidById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "forsaken_thicket";

        var (actual, _) = await sut.Raids.GetRaidById(id);

        Assert.Equal(id, actual.Id);
    }
}
