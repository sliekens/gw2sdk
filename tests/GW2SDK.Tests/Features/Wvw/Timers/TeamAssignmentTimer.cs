using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Timers;

namespace GuildWars2.Tests.Features.Wvw.Timers;

public class TeamAssignmentTimer
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (WvwTimer actual, MessageContext context) = await sut.Wvw.GetTeamAssignmentTimer(TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.True(actual.NorthAmerica > DateTimeOffset.UtcNow);
        Assert.True(actual.Europe > DateTimeOffset.UtcNow);
    }
}
