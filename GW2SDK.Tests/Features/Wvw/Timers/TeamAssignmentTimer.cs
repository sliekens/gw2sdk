using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Timers;

public class TeamAssignmentTimer
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetTeamAssignmentTimer();

        Assert.NotNull(context);
        Assert.True(actual.NorthAmerica > DateTimeOffset.UtcNow);
        Assert.True(actual.Europe > DateTimeOffset.UtcNow);
    }
}
