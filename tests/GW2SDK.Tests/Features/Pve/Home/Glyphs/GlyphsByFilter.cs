using GuildWars2.Pve.Home.Decorations;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Pve.Home.Glyphs;

public class GlyphsByFilter
{

    [Test]

    public async Task Can_be_filtered_by_id()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = ["leatherworker_logging", "watchknight_harvesting", "unbound_mining"];

        (HashSet<Glyph> actual, MessageContext context) = await sut.Pve.Home.GetGlyphsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(ids.Count, context.ResultCount);

        Assert.True(context.ResultTotal > ids.Count);

        Assert.Equal(ids.Count, actual.Count);

        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
