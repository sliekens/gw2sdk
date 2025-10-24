using GuildWars2.Items.Stats;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Items.Stats;

public class AttributeCombinationById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 559;

        (AttributeCombination actual, MessageContext context) = await sut.Items.GetAttributeCombinationById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
