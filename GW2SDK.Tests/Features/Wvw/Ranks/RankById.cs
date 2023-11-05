using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

public class RankById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 105;

        var (actual, _) = await sut.Wvw.GetRankById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_title();
        actual.Has_min_rank();
    }
}
