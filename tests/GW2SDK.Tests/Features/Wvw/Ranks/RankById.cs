using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

[ServiceDataSource]
public class RankById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 105;
        (Rank actual, MessageContext context) = await sut.Wvw.GetRankById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
