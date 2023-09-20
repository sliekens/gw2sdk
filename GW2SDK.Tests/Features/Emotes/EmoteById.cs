using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emotes;

public class EmoteById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "rockout";

        var actual = await sut.Emotes.GetEmoteById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_commands();
        actual.Value.Has_unlock_items();
    }
}
