using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1001;

        var actual = await sut.Worlds.GetWorldById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}