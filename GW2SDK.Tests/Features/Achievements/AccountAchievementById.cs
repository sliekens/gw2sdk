using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AccountAchievementById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        const int id = 1;

        var actual =
            await sut.Achievements.GetAccountAchievementById(id, accessToken.Key);

        Assert.Equal(id, actual.Value.Id);
    }
}
