using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementGroupById
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";
        (AchievementGroup actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroupById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
