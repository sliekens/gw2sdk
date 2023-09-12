using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emotes;

public class UnlockedEmotes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Emotes.GetUnlockedEmotes(accessToken.Key);

        // Can be empty if you haven't unlocked any emotes
        // The best we can do is verify that there are no unexpected emotes
        Assert.All(
            actual.Value,
            chest => Assert.Contains(
                chest,
                new[]
                {
                    "Shiverplus",
                    "Stretch",
                    "geargrind",
                    "playdead",
                    "rockout",
                    "shiver",
                    "shuffle",
                    "step"
                }
            )
        );
    }
}
