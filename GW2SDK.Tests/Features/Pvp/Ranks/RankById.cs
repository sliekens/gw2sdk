using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

public class RankById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 4;

        var (actual, _) = await sut.Pvp.GetRankById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_icon();
        actual.Has_levels();
    }
}
