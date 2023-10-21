using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Heroes;

public class HeroesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "115C140F-C2F5-40EB-8EA2-C3773F2AE468",
            "B7EA9889-5F16-4636-9705-4FCAF8B39ECD",
            "BEA79596-CA8B-4D46-9B9C-EA1B606BCF42"
        };

        var actual = await sut.Pvp.GetHeroesByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
