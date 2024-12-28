using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Progression
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Account.GetProgression(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Contains(
            actual,
            progression => progression is { Id: ProgressionKind.FractalAgonyImpedance, Value: > 0 }
        );
        Assert.Contains(
            actual,
            progression => progression is { Id: ProgressionKind.FractalEmpowerment, Value: > 0 }
        );
        Assert.Contains(
            actual,
            progression =>
                progression is { Id: ProgressionKind.FractalKarmicRetribution, Value: > 0 }
        );
        Assert.Contains(
            actual,
            progression => progression is { Id: ProgressionKind.FractalMistAttunement, Value: > 0 }
        );
        Assert.Contains(
            actual,
            progression => progression is { Id: ProgressionKind.Luck, Value: > 100 }
        );
    }
}
