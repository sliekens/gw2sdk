using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Home;

public class CatById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 20;

        var actual = await sut.Home.GetCatById(id);

        Assert.NotNull(actual.Value);
        Assert.Equal(20, actual.Value.Id);
        Assert.Equal("necromancer", actual.Value.Hint);
    }
}
