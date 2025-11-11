using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Timers;

namespace GuildWars2.Tests.Features.Wvw.Timers;

[ServiceDataSource]
public class TeamAssignmentTimer(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        (WvwTimer actual, MessageContext context) = await sut.Wvw.GetTeamAssignmentTimer(TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.True(actual.NorthAmerica > DateTimeOffset.UtcNow);
        Assert.True(actual.Europe > DateTimeOffset.UtcNow);
    }
}
