using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class CatById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 20;

        var (actual, _) = await sut.Pve.Home.GetCatById(id);

        Assert.NotNull(actual);
        Assert.Equal(id, actual.Id);
        Assert.Equal("necromancer", actual.Hint);
    }
}
