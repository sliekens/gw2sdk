using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemBackgroundById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Guilds.GetEmblemBackgroundById(id);

        Assert.Equal(id, actual.Id);
    }
}
