using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Worlds;

public class Worlds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Worlds.GetWorlds();

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            world =>
            {
                Assert.True(world.Id > 0);
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

                if (world.Id >= 2100)
                {
                    Assert.NotEqual(Language.English, world.Language);
                }
            }
        );

        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultCount, actual.Count);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
    }
}
