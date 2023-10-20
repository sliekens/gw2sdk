using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Masteries;

public class MasteryPointsProgress
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Masteries.GetMasteryPointsProgress(accessToken.Key);

        Assert.NotNull(actual.Value);
        Assert.Equal(actual.Value.Unlocked.Count, actual.Value.Totals.Sum(total => total.Earned));
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "Central Tyria"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "Heart of Thorns"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "Path of Fire"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "Icebrood Saga"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "End of Dragons"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Value.Totals,
            total => total.Region == "Secrets of the Obscure"
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
    }
}
