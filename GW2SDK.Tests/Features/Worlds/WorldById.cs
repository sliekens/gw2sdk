using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1001;

        var (actual, _) = await sut.Worlds.GetWorldById(id);

        Assert.Equal(id, actual.Id);
        Assert.NotEmpty(actual.Name);
        if (actual.Population != WorldPopulation.Full)
        {
            switch (actual.Population)
            {
                case WorldPopulation.Medium:
                    Assert.Equal(500, actual.TransferFee);
                    break;
                case WorldPopulation.High:
                    Assert.Equal(1000, actual.TransferFee);
                    break;
                case WorldPopulation.VeryHigh:
                    Assert.Equal(1800, actual.TransferFee);
                    break;
                default:
                    throw new Exception("Unexpected population type.");
            }
        }

        Assert.Equal(WorldRegion.NorthAmerica, actual.Region);
    }
}
