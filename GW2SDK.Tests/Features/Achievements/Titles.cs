using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class Titles
{
    [Fact]
    public async Task Titles_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetTitles();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Can_be_unlocked_by_achievements();
            }
        );
    }
}
