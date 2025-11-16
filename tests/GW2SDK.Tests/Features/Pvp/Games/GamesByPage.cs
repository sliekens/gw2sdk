using GuildWars2.Pvp.Games;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.Games;

[ServiceDataSource]
public class GamesByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        const int pageSize = 3;
        (HashSet<Game> actual, MessageContext context) = await sut.Pvp.GetGamesByPage(0, pageSize, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, ps => ps.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            await Assert.That(actual).HasCount(pageSize);
            foreach (Game entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}
