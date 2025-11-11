using GuildWars2.Guilds.Permissions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

[ServiceDataSource]
public class GuildPermissions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<GuildPermissionSummary> actual, MessageContext context) = await sut.Guilds.GetGuildPermissions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Name);
            Assert.NotEmpty(entry.Description);
        });
    }
}
