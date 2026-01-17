using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class SpecializationsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<Specialization> actual, MessageContext context) = await sut.Hero.Builds.GetSpecializationsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (int id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
