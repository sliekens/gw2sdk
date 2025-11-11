using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

[ServiceDataSource]
public class Glyphs(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Glyph> actual, MessageContext context) = await sut.Pve.Home.GetGlyphs(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, glyph =>
        {
            Assert.NotNull(glyph);
            Assert.NotEmpty(glyph.Id);
        });
    }
}
