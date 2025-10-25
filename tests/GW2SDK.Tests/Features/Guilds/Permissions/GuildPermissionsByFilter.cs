using GuildWars2.Guilds.Permissions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

public class GuildPermissionsByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        HashSet<string> ids = ["StartingRole", "DepositCoinsTrove", "WithdrawCoinsTrove"];
        (HashSet<GuildPermissionSummary> actual, MessageContext context) = await sut.Guilds.GetGuildPermissionsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
