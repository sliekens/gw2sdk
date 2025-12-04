using GuildWars2.Pvp.Games;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pvp.Games;

[ServiceDataSource]
public class Games(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<Game> actual, MessageContext context) = await sut.Pvp.GetGames(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            foreach (Game entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.RatingType.IsDefined()).IsTrue();
                await Assert.That(entry.Result.IsDefined()).IsTrue();
                await Assert.That(entry.Profession.IsDefined()).IsTrue();
            }
        }
    }
}
