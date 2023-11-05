using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AchievementGroupsByPage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.Hero.Achievements.GetAchievementGroupsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(pageSize, context.PageContext.PageSize);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(pageSize, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
                entry.Has_order();
                entry.Has_categories();
            }
        );
    }
}
