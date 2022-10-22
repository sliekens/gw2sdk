using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Guilds;

public class GuildPermissionById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string guildPermissionId = "StartingRole";

        var actual = await sut.Guilds.GetGuildPermissionById(guildPermissionId);

        Assert.Equal(guildPermissionId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
    }
}
