using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Progression
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Account.GetProgression(accessToken.Key);

        Assert.Contains(
            actual,
            progression => progression.Id == ProgressionKind.FractalAgonyImpedance
                && progression.Value > 0
        );
        Assert.Contains(
            actual,
            progression => progression.Id == ProgressionKind.FractalEmpowerment
                && progression.Value > 0
        );
        Assert.Contains(
            actual,
            progression => progression.Id == ProgressionKind.FractalKarmicRetribution
                && progression.Value > 0
        );
        Assert.Contains(
            actual,
            progression => progression.Id == ProgressionKind.FractalMistAttunement
                && progression.Value > 0
        );
        Assert.Contains(
            actual,
            progression => progression.Id == ProgressionKind.Luck && progression.Value > 100
        );
    }
}
