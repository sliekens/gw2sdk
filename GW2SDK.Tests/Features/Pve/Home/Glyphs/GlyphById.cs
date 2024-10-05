using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class GlyphById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "leatherworker_logging";

        var (actual, context) = await sut.Pve.Home.GetGlyphById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
