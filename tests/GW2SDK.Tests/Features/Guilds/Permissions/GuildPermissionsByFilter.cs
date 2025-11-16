using GuildWars2.Guilds.Permissions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

[ServiceDataSource]
public class GuildPermissionsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["StartingRole", "DepositCoinsTrove", "WithdrawCoinsTrove"];
        (HashSet<GuildPermissionSummary> actual, MessageContext context) = await sut.Guilds.GetGuildPermissionsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(ids.Count));
        await Assert.That(actual).HasCount().EqualTo(ids.Count);
        using (Assert.Multiple())
        {
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
