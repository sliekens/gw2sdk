using GuildWars2.Items.Stats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Items.Stats;

[ServiceDataSource]
public class AttributeCombinations(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<AttributeCombination> actual, MessageContext context) = await sut.Items.GetAttributeCombinations(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultTotal, actual.Count);
    }
}
