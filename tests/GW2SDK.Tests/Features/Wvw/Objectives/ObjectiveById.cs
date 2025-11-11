using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

[ServiceDataSource]
public class ObjectiveById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1099-99";
        (Objective actual, MessageContext context) = await sut.Wvw.GetObjectiveById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
