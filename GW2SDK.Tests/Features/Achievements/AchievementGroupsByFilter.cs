using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementGroupsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "A4ED8379-5B6B-4ECC-B6E1-70C350C902D2",
            "56A82BB9-6B07-4AB0-89EE-E4A6D68F5C47",
            "B42E2379-9599-46CA-9D4A-40A27E192BBE"
        };

        var actual = await sut.Achievements.GetAchievementGroupsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
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
