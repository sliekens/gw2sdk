using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemForegroundsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var (actual, _) = await sut.Guilds.GetEmblemForegroundsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.All(ids, id => Assert.Contains(id, actual.Select(value => value.Id)));
    }
}
