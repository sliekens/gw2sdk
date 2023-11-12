using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class DungeonById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "citadel_of_flame";

        var (actual, _) = await sut.Pve.Dungeons.GetDungeonById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_paths();
    }
}
