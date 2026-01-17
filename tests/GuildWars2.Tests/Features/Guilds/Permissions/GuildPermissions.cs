using GuildWars2.Guilds.Permissions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

[ServiceDataSource]
public class GuildPermissions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<GuildPermissionSummary> actual, MessageContext context) = await sut.Guilds.GetGuildPermissions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (GuildPermissionSummary entry in actual)
            {
                await Assert.That(entry.Id).IsNotEmpty();
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.Description).IsNotEmpty();
            }
        }
    }
}
