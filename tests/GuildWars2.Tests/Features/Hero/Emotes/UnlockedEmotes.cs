using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Emotes;

[ServiceDataSource]
public class UnlockedEmotes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<string> actual, _) = await sut.Hero.Emotes.GetUnlockedEmotes(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        // Can be empty if you haven't unlocked any emotes
        // The best we can do is verify that there are no unexpected emotes
        foreach (string chest in actual)
        {
            await Assert.That(new[] { "Bless", "geargrind", "Heroic", "Paper", "playdead", "Possessed", "Rock", "rockout", "Scissors", "shiver", "Shiverplus", "shuffle", "step", "Stretch" }).Contains(chest);
        }
    }
}
