using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class Glyphs
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.Home.GetGlyphs(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            glyph =>
            {
                Assert.NotNull(glyph);
                Assert.NotEmpty(glyph.Id);
            }
        );
    }
}
