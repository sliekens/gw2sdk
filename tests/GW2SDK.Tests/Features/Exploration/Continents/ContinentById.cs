using GuildWars2.Exploration.Continents;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Continents;

[ServiceDataSource]
public class ContinentById(Gw2Client sut)
{
    [Test]
    [Arguments(1)]
    [Arguments(2)]
    public async Task Can_be_found(int id)
    {
        (Continent actual, MessageContext context) = await sut.Exploration.GetContinentById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
