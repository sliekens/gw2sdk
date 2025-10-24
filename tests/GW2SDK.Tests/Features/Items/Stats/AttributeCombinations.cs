using GuildWars2.Items.Stats;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Items.Stats;

public class AttributeCombinations
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<AttributeCombination> actual, MessageContext context) = await sut.Items.GetAttributeCombinations(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
