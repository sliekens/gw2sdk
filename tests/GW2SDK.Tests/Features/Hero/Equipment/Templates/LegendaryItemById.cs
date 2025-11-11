using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

[ServiceDataSource]
public class LegendaryItemById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found_by_id()
    {
        const int id = 80111;
        (LegendaryItem actual, MessageContext context) = await sut.Hero.Equipment.Templates.GetLegendaryItemById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
