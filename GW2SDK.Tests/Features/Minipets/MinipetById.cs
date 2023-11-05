using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Minipets;

public class MinipetById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Minipets.GetMinipetById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_icon();
        actual.Has_order();
        actual.Has_item_id();
    }
}
