using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Dungeons;

public class DungeonsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "twilight_arbor",
            "sorrows_embrace",
            "citadel_of_flame"
        };

        var actual = await sut.Dungeons.GetDungeonsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
                entry.Has_paths();
                Assert.All(
                    entry.Paths,
                    path =>
                    {
                        path.Has_id();
                        path.Has_kind();
                    }
                );
            }
        );
    }
}
