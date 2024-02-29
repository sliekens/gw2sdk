using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementGroupsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2", "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
            "B42E2379-9599-46CA-9D4A-40A27E192BBE"
        ];

        var (actual, context) = await sut.Hero.Achievements.GetAchievementGroupsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
                entry.Has_name();
                entry.Has_description();
                entry.Has_order();
                entry.Has_categories();
            }
        );
    }
}
