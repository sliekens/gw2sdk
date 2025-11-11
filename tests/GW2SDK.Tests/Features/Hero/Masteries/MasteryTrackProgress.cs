using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Masteries;

[ServiceDataSource]
public class MasteryTrackProgress(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<GuildWars2.Hero.Masteries.MasteryTrackProgress> actual, _) = await sut.Hero.Masteries.GetMasteryTrackProgress(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, progress =>
        {
            Assert.True(progress.Id > 0);
            Assert.True(progress.Level > 0);
        });
    }
}
