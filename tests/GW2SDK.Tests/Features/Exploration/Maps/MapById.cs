using GuildWars2.Exploration.Maps;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapById
{

    [Test]

    [Arguments(1, 0, 1, 26)]

    [Arguments(1, 0, 1, 27)]

    [Arguments(1, 0, 1, 28)]

    public async Task Can_be_found(int continentId, int floorId, int regionId, int mapId)
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (Map actual, MessageContext context) = await sut.Exploration.GetMapById(continentId, floorId, regionId, mapId, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(mapId, actual.Id);
    }
}
