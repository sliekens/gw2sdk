using GuildWars2.Exploration.Continents;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentById
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_found(int id)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (Continent actual, MessageContext context) = await sut.Exploration.GetContinentById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
