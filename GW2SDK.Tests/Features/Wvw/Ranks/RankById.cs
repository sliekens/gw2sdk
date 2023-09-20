using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

public class RankById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 105;

        var actual = await sut.Wvw.GetRankById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_title();
        actual.Value.Has_min_rank();
    }
}
