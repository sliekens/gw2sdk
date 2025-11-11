using GuildWars2.Guilds.Permissions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

[ServiceDataSource]
public class GuildPermissionById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "StartingRole";
        (GuildPermissionSummary actual, MessageContext context) = await sut.Guilds.GetGuildPermissionById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
