using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AchievementGroupsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int pageSize = 3;
        (HashSet<AchievementGroup> actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroupsByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        foreach (AchievementGroup entry in actual)
        {
            await Assert.That(entry).IsNotNull();
        }
    }
}
