using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Worlds;

[ServiceDataSource]
public class Worlds(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<World> actual, MessageContext context) = await sut.Worlds.GetWorlds(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (World world in actual)
            {
                await Assert.That(world.Id).IsGreaterThan(0);
                await Assert.That(world.Name).IsNotEmpty();
                if (world.Population != WorldPopulation.Full)
                {
                    switch (world.Population.ToEnum())
                    {
                        case WorldPopulation.None:
                            await Assert.That(world.TransferFee).IsEqualTo(0);
                            break;
                        case WorldPopulation.Low:
                            await Assert.That(world.TransferFee).IsEqualTo(0);
                            break;
                        case WorldPopulation.Medium:
                            await Assert.That(world.TransferFee).IsEqualTo(500);
                            break;
                        case WorldPopulation.High:
                            await Assert.That(world.TransferFee).IsEqualTo(1000);
                            break;
                        case WorldPopulation.VeryHigh:
                            await Assert.That(world.TransferFee).IsEqualTo(1800);
                            break;
                        case WorldPopulation.Full:
                            await Assert.That(world.TransferFee).IsEqualTo(0);
                            break;
                        case null:
                            break;
                        default:
                            throw new InvalidOperationException("Unexpected population type.");
                    }
                }

                await Assert.That(world.Region).IsNotEqualTo(WorldRegion.None);
                if (world.Id >= 2100)
                {
                    await Assert.That(world.Language).IsNotEqualTo(Language.English);
                }
            }
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        }
    }
}
