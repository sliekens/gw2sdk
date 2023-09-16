using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Races;

public class RaceByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const RaceName name = RaceName.Human;

        var actual = await sut.Races.GetRaceByName(name);

        Assert.Equal(name, actual.Value.Id);
    }
}
