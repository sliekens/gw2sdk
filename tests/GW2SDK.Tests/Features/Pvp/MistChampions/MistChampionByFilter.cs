using GuildWars2.Pvp.MistChampions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class MistChampionByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        using (Assert.Multiple())
        {
            HashSet<string> ids = ["115C140F-C2F5-40EB-8EA2-C3773F2AE468", "B7EA9889-5F16-4636-9705-4FCAF8B39ECD", "BEA79596-CA8B-4D46-9B9C-EA1B606BCF42"];
            (HashSet<MistChampion> actual, MessageContext context) = await sut.Pvp.GetMistChampionsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual.Count).IsEqualTo(ids.Count);
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
