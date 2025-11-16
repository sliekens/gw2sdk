using GuildWars2.Logos;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Logos;

[ServiceDataSource]
public class LogoById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "Guild-Wars-2-logo-en";
        (Logo actual, MessageContext context) = await sut.Logos.GetLogoById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(id);
        }
    }
}
