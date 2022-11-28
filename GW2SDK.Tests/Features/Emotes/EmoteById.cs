using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emotes;

public class EmoteById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string emoteId = "rockout";

        var actual = await sut.Emotes.GetEmoteById(emoteId);

        Assert.Equal(emoteId, actual.Value.Id);
        actual.Value.Has_commands();
        actual.Value.Has_unlock_items();
    }
}
