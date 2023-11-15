using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1001,
            1002,
            1003
        };

        var (actual, context) = await sut.Worlds.GetWorldsByIds(ids);

        Assert.All(ids,
            id =>
            {
                var world = actual.Single(world => world.Id == id);
                Assert.NotEmpty(world.Name);
                if (world.Population != WorldPopulation.Full)
                {
                    switch (world.Population)
                    {
                        case WorldPopulation.Medium:
                            Assert.Equal(500, world.TransferFee);
                            break;
                        case WorldPopulation.High:
                            Assert.Equal(1000, world.TransferFee);
                            break;
                        case WorldPopulation.VeryHigh:
                            Assert.Equal(1800, world.TransferFee);
                            break;
                        default:
                            throw new Exception("Unexpected population type.");
                    }
                }

                Assert.NotEqual(WorldRegion.None, world.Region);
            });

        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.NotEqual(actual.Count, context.ResultContext.ResultTotal);
    }
}
