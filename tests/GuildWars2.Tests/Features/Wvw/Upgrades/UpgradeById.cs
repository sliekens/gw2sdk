using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

[ServiceDataSource]
public class UpgradeById(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_found()
    {
        const int id = 57;
        (ObjectiveUpgrade actual, MessageContext context) = await sut.Wvw.GetUpgradeById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
