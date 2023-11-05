using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emblems;

public class ForegroundEmblemById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var (actual, _) = await sut.Emblems.GetForegroundEmblemById(id);

        Assert.Equal(id, actual.Id);
    }
}
