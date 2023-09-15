using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emblems;

public class BackgroundEmblemsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Emblems.GetBackgroundEmblemsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.All(ids, id => Assert.Contains(id, actual.Value.Select(value => value.Id)));
    }
}
