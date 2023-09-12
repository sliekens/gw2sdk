using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Achievements;

public class AccountAchievements
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Achievements.GetAccountAchievements(accessToken.Key);

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);

        Assert.All(
            actual.Value,
            achievement =>
            {
                achievement.Has_id();
            }
        );
    }

}
