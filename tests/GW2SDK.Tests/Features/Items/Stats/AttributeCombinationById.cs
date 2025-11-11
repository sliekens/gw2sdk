using GuildWars2.Items.Stats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items.Stats;

[ServiceDataSource]
public class AttributeCombinationById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 559;
        (AttributeCombination actual, MessageContext context) = await sut.Items.GetAttributeCombinationById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
