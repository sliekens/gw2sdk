using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummaryById
{
    [Theory]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(18)]
    public async Task Can_be_found(int id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        (MapSummary actual, MessageContext context) = await sut.Exploration.GetMapSummaryById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
