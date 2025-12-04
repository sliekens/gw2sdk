using System.Globalization;

using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Timers;

namespace GuildWars2.Tests.Features.Wvw.Timers;

[ServiceDataSource]
public class LockoutTimer(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        (WvwTimer actual, MessageContext context) = await sut.Wvw.GetLockoutTimer(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.NorthAmerica >= DateTimeOffset.Parse("2024-10-23T17:00:00Z", CultureInfo.InvariantCulture)).IsTrue();
        await Assert.That(actual.Europe >= DateTimeOffset.Parse("2024-10-23T17:00:00Z", CultureInfo.InvariantCulture)).IsTrue();
    }
}
