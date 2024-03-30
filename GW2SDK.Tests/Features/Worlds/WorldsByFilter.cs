using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1001, 1002,
            1003
        ];

        var (actual, context) = await sut.Worlds.GetWorldsByIds(ids);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
