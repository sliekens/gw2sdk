using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Finishers;

public class FinisherById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 58;

        var (actual, _) = await sut.Hero.Finishers.GetFinisherById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_unlock_details();
        actual.Has_unlock_items();
        actual.Has_order();
        actual.Has_icon();
        actual.Has_name();
    }
}
