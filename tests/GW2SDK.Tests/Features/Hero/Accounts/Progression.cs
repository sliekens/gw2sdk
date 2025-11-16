using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class Progression(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<GuildWars2.Hero.Accounts.Progression> actual, _) = await sut.Hero.Account.GetProgression(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken).ConfigureAwait(true);
        await Assert.That(actual).Contains(progression => progression is { Id: ProgressionKind.FractalAgonyImpedance, Value: > 0 });
        await Assert.That(actual).Contains(progression => progression is { Id: ProgressionKind.FractalEmpowerment, Value: > 0 });
        await Assert.That(actual).Contains(progression => progression is { Id: ProgressionKind.FractalKarmicRetribution, Value: > 0 });
        await Assert.That(actual).Contains(progression => progression is { Id: ProgressionKind.FractalMistAttunement, Value: > 0 });
        await Assert.That(actual).Contains(progression => progression is { Id: ProgressionKind.Luck, Value: > 100 });
    }
}
