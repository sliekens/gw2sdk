using GuildWars2.Pvp.Ranks;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

public class RankById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 4;

        (Rank actual, MessageContext context) = await sut.Pvp.GetRankById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
