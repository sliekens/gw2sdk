using GuildWars2.Guilds.Emblems;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

[ServiceDataSource]
public class EmblemForegroundById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (EmblemForeground actual, MessageContext context) = await sut.Guilds.GetEmblemForegroundById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
