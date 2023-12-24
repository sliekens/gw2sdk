using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Masteries;

public class MasteryTrackProgress
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Masteries.GetMasteryTrackProgress(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            progress =>
            {
                Assert.True(progress.Id > 0);
                Assert.True(progress.Level > 0);
            }
        );
    }
}
