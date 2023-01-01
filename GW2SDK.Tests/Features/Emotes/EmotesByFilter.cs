using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

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

        var actual = await sut.Emotes.GetEmotesByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Id_is_not_empty();
                entry.Has_commands();
                entry.Has_unlock_items();
            }
        );
    }
}
