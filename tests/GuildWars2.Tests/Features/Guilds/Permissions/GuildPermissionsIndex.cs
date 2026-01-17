using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

[ServiceDataSource]
public class GuildPermissionsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<string> actual, MessageContext context) = await sut.Guilds.GetGuildPermissionsIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
