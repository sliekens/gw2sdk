using GuildWars2.Hero.Equipment.Gliders;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class GliderSkinById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 58;

        (GliderSkin actual, MessageContext context) = await sut.Hero.Equipment.Gliders.GetGliderSkinById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
