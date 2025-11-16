using GuildWars2.Pvp.Seasons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class Seasons(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Season> actual, MessageContext context) = await sut.Pvp.GetSeasons(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(actual).IsNotEmpty();
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
            foreach (Season entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.Name).IsNotEmpty();
                if (entry.Leaderboards.Legendary is not null)
                {
                    await Assert.That(entry.Leaderboards.Legendary.Settings.Name).IsEmpty();
                    await Assert.That(entry.Leaderboards.Legendary.Settings.ScoringId).IsNotEmpty();
                    foreach (LeaderboardTier tier in entry.Leaderboards.Legendary.Settings.Tiers)
                    {
                        await Assert.That(tier.Name).IsEmpty();
                        if (tier.Kind.HasValue)
                        {
                            await Assert.That(tier.Kind.Value.IsDefined()).IsTrue();
                        }
                    }
                }

                if (entry.Leaderboards.Guild is not null)
                {
                    await Assert.That(entry.Leaderboards.Guild.Settings.Name).IsEmpty();
                    await Assert.That(entry.Leaderboards.Guild.Settings.ScoringId).IsNotEmpty();
                    foreach (LeaderboardTier tier in entry.Leaderboards.Guild.Settings.Tiers)
                    {
                        await Assert.That(tier.Name).IsNotEmpty();
                        if (tier.Kind.HasValue)
                        {
                            await Assert.That(tier.Kind.Value.IsDefined()).IsTrue();
                        }
                    }
                }

                if (entry.Leaderboards.Ladder is not null)
                {
                    await Assert.That(entry.Leaderboards.Ladder.Settings.Name).IsEmpty();
                    await Assert.That(entry.Leaderboards.Ladder.Settings.ScoringId).IsNotEmpty();
                    foreach (LeaderboardTier tier in entry.Leaderboards.Ladder.Settings.Tiers)
                    {
                        await Assert.That(tier.Name).IsEmpty();
                        if (tier.Kind.HasValue)
                        {
                            await Assert.That(tier.Kind.Value.IsDefined()).IsTrue();
                        }
                    }
                }
            }
        }
    }
}
