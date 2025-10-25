using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Masteries;

public class MasteryTrackProgress
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<GuildWars2.Hero.Masteries.MasteryTrackProgress> actual, _) = await sut.Hero.Masteries.GetMasteryTrackProgress(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, progress =>
        {
            Assert.True(progress.Id > 0);
            Assert.True(progress.Level > 0);
        });
    }
}
