using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Timers;

public class LockoutTimer
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetLockoutTimer(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.True(actual.NorthAmerica >= DateTimeOffset.Parse("2024-10-23T17:00:00Z"));
        Assert.True(actual.Europe >= DateTimeOffset.Parse("2024-10-23T17:00:00Z"));
    }
}
