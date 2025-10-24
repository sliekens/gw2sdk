using GuildWars2.Exploration.Continents;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsByFilter
{

    [Test]

    public async Task Can_be_filtered_by_id()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = [1, 2];

        (HashSet<Continent> actual, MessageContext context) = await sut.Exploration.GetContinentsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(ids.Count, context.ResultCount);

        Assert.Equal(ids.Count, context.ResultTotal);

        Assert.Equal(ids.Count, actual.Count);

        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second));
    }
}
