using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Races;

public class RacesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Extensible<RaceName>> actual, MessageContext context) =
            await sut.Hero.Races.GetRacesIndex(TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
