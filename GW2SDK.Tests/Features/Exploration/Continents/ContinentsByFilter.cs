using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2
        };

        var actual = await sut.Maps.GetContinentsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_dimensions();
                entry.Has_min_zoom();
                entry.Has_max_zoom();
                entry.Has_floors();
            }
        );
    }
}
