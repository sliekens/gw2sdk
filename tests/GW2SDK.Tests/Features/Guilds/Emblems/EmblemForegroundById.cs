using GuildWars2.Guilds.Emblems;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemForegroundById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        (EmblemForeground actual, MessageContext context) = await sut.Guilds.GetEmblemForegroundById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
