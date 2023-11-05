using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emotes;

public class EmotesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "geargrind",
            "playdead",
            "rockout"
        };

        var (actual, context) = await sut.Emotes.GetEmotesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
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
