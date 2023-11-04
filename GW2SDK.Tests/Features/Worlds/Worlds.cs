using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Worlds;

public class Worlds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Worlds.GetWorlds();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            world =>
            {
                world.Id_is_positive();
                world.Name_is_not_empty();
                world.World_population_type_is_supported();
            }
        );
    }
}
