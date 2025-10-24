using GuildWars2.Logos;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Logos;

public class LogoById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const string id = "Guild-Wars-2-logo-en";

        (Logo actual, MessageContext context) = await sut.Logos.GetLogoById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
