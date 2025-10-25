using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class Glyphs
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Glyph> actual, MessageContext context) = await sut.Pve.Home.GetGlyphs(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, glyph =>
        {
            Assert.NotNull(glyph);
            Assert.NotEmpty(glyph.Id);
        });
    }
}
