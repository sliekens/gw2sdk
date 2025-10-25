using GuildWars2.Pve.Dungeons;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class DungeonsByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        HashSet<string> ids = ["twilight_arbor", "sorrows_embrace", "citadel_of_flame"];
        (HashSet<Dungeon> actual, MessageContext context) = await sut.Pve.Dungeons.GetDungeonsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
