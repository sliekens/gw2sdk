using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.WizardsVault.Objectives;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

[ServiceDataSource]
public class Objectives(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Objective> actual, MessageContext context) = await sut.WizardsVault.GetObjectives(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            foreach (Objective objective in actual)
            {
                await Assert.That(objective)
                    .Member(o => o.Id, id => id.IsGreaterThan(0))
                    .And.Member(o => o.Title, title => title.IsNotEmpty())
                    .And.Member(o => o.RewardAcclaim, acclaim => acclaim.IsGreaterThan(0));
                await Assert.That(objective.Track.IsDefined()).IsTrue();
            }
        }
    }
}
