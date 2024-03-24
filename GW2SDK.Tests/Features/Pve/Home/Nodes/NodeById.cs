using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

public class NodeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "bauble_gathering_system";

        var (actual, _) = await sut.Pve.Home.GetNodeById(id);

        Assert.NotNull(actual);
        Assert.Equal(id, actual.Id);
    }
}
