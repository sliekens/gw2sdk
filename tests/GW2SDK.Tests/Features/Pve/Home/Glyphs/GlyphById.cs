using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

[ServiceDataSource]
public class GlyphById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "leatherworker_logging";
        (Glyph actual, MessageContext context) = await sut.Pve.Home.GetGlyphById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
