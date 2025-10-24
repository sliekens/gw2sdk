using GuildWars2.Pve.Home.Decorations;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationCategoryById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (DecorationCategory actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategoryById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
