using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Permissions;

public class GuildPermissionById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "StartingRole";

        var (actual, _) = await sut.Guilds.GetGuildPermissionById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_description();
    }
}
