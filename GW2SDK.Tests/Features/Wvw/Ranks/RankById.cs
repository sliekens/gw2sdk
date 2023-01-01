using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

public class RankById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int rankId = 105;

        var actual = await sut.Wvw.GetRankById(rankId);

        Assert.Equal(rankId, actual.Value.Id);
        actual.Value.Has_title();
        actual.Value.Has_min_rank();
    }
}
