using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.Games;

[ServiceDataSource]
public class GamesIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IImmutableValueSet<string> actual, MessageContext context) = await sut.Pvp.GetGamesIndex(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            await Assert.That(actual).IsNotEmpty();
        }
    }
}
