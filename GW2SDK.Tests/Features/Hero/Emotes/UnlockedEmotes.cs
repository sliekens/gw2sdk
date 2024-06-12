using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class UnlockedEmotes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Emotes.GetUnlockedEmotes(accessToken.Key);

        // Can be empty if you haven't unlocked any emotes
        // The best we can do is verify that there are no unexpected emotes
        Assert.All(
            actual,
            chest => Assert.Contains(
                chest,
                new[]
                {
                    "Bless",
                    "geargrind",
                    "Heroic",
                    "Paper",
                    "playdead",
                    "Rock",
                    "rockout",
                    "Scissors",
                    "shiver",
                    "Shiverplus",
                    "shuffle",
                    "step",
                    "Stretch"
                }
            )
        );
    }
}
