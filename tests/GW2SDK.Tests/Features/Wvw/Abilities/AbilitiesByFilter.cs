using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

[ServiceDataSource]
public class AbilitiesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [2, 3, 4];
        (HashSet<Ability> actual, MessageContext context) = await sut.Wvw.GetAbilitiesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).HasCount(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
