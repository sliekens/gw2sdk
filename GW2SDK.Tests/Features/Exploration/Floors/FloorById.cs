using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorById
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, 1)]
    [InlineData(1, 2)]
    [InlineData(2, 1)]
    [InlineData(2, 3)]
    [InlineData(2, 5)]
    public async Task Can_be_found(int continentId, int floorId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetFloorById(continentId, floorId, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(floorId, actual.Id);
    }
}
