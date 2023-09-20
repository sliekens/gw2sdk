using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Minipets;

public class MinipetById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Minipets.GetMinipetById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_order();
        actual.Value.Has_item_id();
    }
}
