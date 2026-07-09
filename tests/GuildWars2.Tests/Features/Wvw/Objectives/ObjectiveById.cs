using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

[ServiceDataSource]
public class ObjectiveById(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1099-99";
        (Objective actual, MessageContext context) = await sut.Wvw.GetObjectiveById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
