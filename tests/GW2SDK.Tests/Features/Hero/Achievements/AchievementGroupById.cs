using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AchievementGroupById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2";
        (AchievementGroup actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroupById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
