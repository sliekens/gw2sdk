using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

public class GuildPermissionById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const GuildPermission guildPermissionId = GuildPermission.StartingRole;

        var actual = await sut.Guilds.GetGuildPermissionById(guildPermissionId);

        Assert.Equal(guildPermissionId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
    }
}
