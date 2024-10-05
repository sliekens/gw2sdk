using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Timers;

public class LockoutTimer
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetLockoutTimer();

        Assert.NotNull(context);
        Assert.True(actual.NorthAmerica > DateTimeOffset.UtcNow);
        Assert.True(actual.Europe > DateTimeOffset.UtcNow);
    }
}
