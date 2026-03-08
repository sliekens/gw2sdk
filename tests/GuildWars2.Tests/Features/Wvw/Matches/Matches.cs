using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

[ServiceDataSource]
public class Matches(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Match> actual, MessageContext context) = await sut.Wvw.GetMatches(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Match entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.StartTime > DateTimeOffset.MinValue).IsTrue();
            await Assert.That(entry.EndTime > entry.StartTime).IsTrue();
            foreach (Skirmish skirmish in entry.Skirmishes)
            {
                await Assert.That(skirmish.Id > 0).IsTrue();
                foreach (MapScores score in skirmish.MapScores)
                {
                    await Assert.That(score.Kind.IsDefined()).IsTrue();
                }
            }
            foreach (Map map in entry.Maps)
            {
                await Assert.That(map.Id > 0).IsTrue();
                await Assert.That(map.Kind.IsDefined()).IsTrue();
                foreach (Bonus bonus in map.Bonuses)
                {
                    await Assert.That(bonus.Kind.IsDefined()).IsTrue();
                    await Assert.That(bonus.Owner.IsDefined()).IsTrue();
                }
                foreach (OwnedObjective objective in map.Objectives)
                {
                    await Assert.That(objective.Id).IsNotEmpty();
                    await Assert.That(objective.LastFlipped > DateTimeOffset.MinValue).IsTrue();
                    await Assert.That(objective.Owner.IsDefined()).IsTrue();
                    if (objective is not OwnedSpawn and not OwnedMercenary and not OwnedRuins)
                    {
                        await Assert.That(objective.PointsCapture > 0).IsTrue();
                        // Temporary assertion for a live API regression: $.maps[*].objectives[*].points_tick is
                        // currently 0 even for scoring objectives such as Camp, Tower, Keep, and Castle.
                        // This should fail once the API starts returning real per-tick scores again.
                        await Assert.That(objective.PointsTick).IsEqualTo(0);
                    }
                }
            }
        }
    }
}
