using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Gliders;

public class GlidersByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            38,
            74
        };

        var actual = await sut.Gliders.GetGlidersByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_unlock_items();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_name();
                entry.Has_description();
                entry.Has_default_dyes();
            }
        );
    }
}
