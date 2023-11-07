using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class EmoteById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "rockout";

        var (actual, _) = await sut.Hero.Emotes.GetEmoteById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_commands();
        actual.Has_unlock_items();
    }
}
