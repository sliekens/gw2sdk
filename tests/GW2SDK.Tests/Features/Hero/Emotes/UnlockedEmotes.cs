using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class UnlockedEmotes
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<string> actual, _) = await sut.Hero.Emotes.GetUnlockedEmotes(accessToken.Key, TestContext.Current!.CancellationToken);
        // Can be empty if you haven't unlocked any emotes
        // The best we can do is verify that there are no unexpected emotes
        Assert.All(actual, chest => Assert.Contains(chest, new[] { "Bless", "geargrind", "Heroic", "Paper", "playdead", "Possessed", "Rock", "rockout", "Scissors", "shiver", "Shiverplus", "shuffle", "step", "Stretch" }));
    }
}
