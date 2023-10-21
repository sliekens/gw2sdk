using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

public class GuildPermissionsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<GuildPermission> ids = new()
        {
            GuildPermission.StartingRole,
            GuildPermission.DepositCoinsTrove,
            GuildPermission.WithdrawCoinsTrove
        };

        var actual = await sut.Guilds.GetGuildPermissionsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
            }
        );
    }
}
