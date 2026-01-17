using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

[ServiceDataSource]
public class Glyphs(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Glyph> actual, MessageContext context) = await sut.Pve.Home.GetGlyphs(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Glyph glyph in actual)
        {
            await Assert.That(glyph).IsNotNull();
            await Assert.That(glyph.Id).IsNotEmpty();
        }
    }
}
