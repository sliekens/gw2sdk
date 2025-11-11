using GuildWars2.Quaggans;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Quaggans;

[ServiceDataSource]
public class QuagganById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "present";
        (Quaggan actual, MessageContext context) = await sut.Quaggans.GetQuagganById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
