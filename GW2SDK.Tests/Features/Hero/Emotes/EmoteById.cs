using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class EmoteById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "rockout";

        var (actual, context) = await sut.Hero.Emotes.GetEmoteById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
