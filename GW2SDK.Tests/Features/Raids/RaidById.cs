using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Raids;

public class RaidById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "forsaken_thicket";

        var actual = await sut.Raids.GetRaidById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
