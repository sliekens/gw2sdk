using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class EmotesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "geargrind", "playdead",
            "rockout"
        ];

        var (actual, context) = await sut.Hero.Emotes.GetEmotesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Id_is_not_empty();
                entry.Has_commands();
                entry.Has_unlock_items();
            }
        );
    }
}
