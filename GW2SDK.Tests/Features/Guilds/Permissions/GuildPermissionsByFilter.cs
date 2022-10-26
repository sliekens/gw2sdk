using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Guilds.Permissions;

public class GuildPermissionsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<GuildPermission> ids = new()
        {
            GuildPermission.StartingRole,
            GuildPermission.DepositCoinsTrove,
            GuildPermission.WithdrawCoinsTrove
        };

        var actual = await sut.Guilds.GetGuildPermissionsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
            }
        );
    }
}
