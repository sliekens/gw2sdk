using GuildWars2.Hero.Races;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Races;

public class Races
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Race> actual, MessageContext context) = await sut.Hero.Races.GetRaces(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
