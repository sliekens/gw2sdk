using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentById
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_found(int id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetContinentById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
