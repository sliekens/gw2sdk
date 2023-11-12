using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items.Stats;

public class ItemStatById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 559;

        var (actual, _) = await sut.Items.GetItemStatById(id);

        Assert.Equal(id, actual.Id);
    }
}
