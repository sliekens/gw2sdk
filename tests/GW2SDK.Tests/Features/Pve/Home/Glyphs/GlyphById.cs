using GuildWars2.Pve.Home.Decorations;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class GlyphById
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const string id = "leatherworker_logging";

        (Glyph actual, MessageContext context) = await sut.Pve.Home.GetGlyphById(id, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(context);

        Assert.Equal(id, actual.Id);
    }
}
