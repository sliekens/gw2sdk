using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Floors;

[ServiceDataSource]
public class FloorsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1)]
    [Arguments(2)]
    public async Task Can_be_listed(int continentId)
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Exploration.GetFloorsIndex(continentId, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}
