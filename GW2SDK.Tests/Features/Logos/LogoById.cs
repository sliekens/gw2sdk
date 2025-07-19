using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Logos;

public class LogoById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "Guild-Wars-2-logo-en";

        var (actual, context) = await sut.Logos.GetLogoById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
