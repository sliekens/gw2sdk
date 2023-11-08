using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class ForegroundEmblemById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Guilds.GetForegroundEmblemById(id);

        Assert.Equal(id, actual.Id);
    }
}
