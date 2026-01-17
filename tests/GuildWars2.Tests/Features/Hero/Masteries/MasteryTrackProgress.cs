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
        (IImmutableValueSet<GuildWars2.Hero.Masteries.MasteryTrackProgress> actual, _) = await sut.Hero.Masteries.GetMasteryTrackProgress(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (GuildWars2.Hero.Masteries.MasteryTrackProgress progress in actual)
        {
            await Assert.That(progress.Id > 0).IsTrue();
            await Assert.That(progress.Level > 0).IsTrue();
        }
    }
}
