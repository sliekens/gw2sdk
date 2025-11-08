using GuildWars2.Exploration.Floors;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorById
{
    [Test]
    [Arguments(1, 0)]
    [Arguments(1, 1)]
    [Arguments(1, 2)]
    [Arguments(2, 1)]
    [Arguments(2, 3)]
    [Arguments(2, 5)]
    public async Task Can_be_found(int continentId, int floorId)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (Floor actual, MessageContext context) = await sut.Exploration.GetFloorById(continentId, floorId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(floorId, actual.Id);
    }
}
