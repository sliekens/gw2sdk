using GuildWars2.Pvp.Ranks;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

[ServiceDataSource]
public class RankById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 4;
        (Rank actual, MessageContext context) = await sut.Pvp.GetRankById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
