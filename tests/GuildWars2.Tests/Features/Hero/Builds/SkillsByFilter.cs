using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class SkillsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1110, 12693, 39222];
        (IImmutableValueSet<Skill> actual, MessageContext context) = await sut.Hero.Builds.GetSkillsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (int id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
