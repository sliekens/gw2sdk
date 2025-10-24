using GuildWars2.Hero.Equipment.Miniatures;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

public class MiniatureById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (Miniature actual, MessageContext context) = await sut.Hero.Equipment.Miniatures.GetMiniatureById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
