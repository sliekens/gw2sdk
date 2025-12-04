using GuildWars2.Files;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Files;

[ServiceDataSource]
public class FileById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "map_vendor_ecto";
        (Asset actual, MessageContext context) = await sut.Files.GetFileById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
