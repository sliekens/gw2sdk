using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Legends;

public class LegendsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "Legend1",
            "Legend3",
            "Legend5"
        };

        var actual = await sut.Legends.GetLegendsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_code();
            }
        );
    }
}
