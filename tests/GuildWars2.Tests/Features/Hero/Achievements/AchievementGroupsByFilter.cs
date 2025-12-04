using GuildWars2.Hero.Achievements.Groups;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AchievementGroupsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47", "B42E2379-9599-46CA-9D4A-40A27E192BBE"];
        (HashSet<AchievementGroup> actual, MessageContext context) = await sut.Hero.Achievements.GetAchievementGroupsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (string id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
