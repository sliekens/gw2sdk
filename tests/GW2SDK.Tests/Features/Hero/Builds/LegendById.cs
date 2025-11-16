using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class LegendById(Gw2Client sut)
{
    [Test]
    [Arguments("Legend1")]
    [Arguments("Legend2")]
    [Arguments("Legend3")]
    public async Task Can_be_found(string id)
    {
        (Legend actual, MessageContext context) = await sut.Hero.Builds.GetLegendById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
