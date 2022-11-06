using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Novelties;

public class Novelties
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Novelties.GetNovelties();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
                entry.Has_icon();
                entry.Has_slot();
                entry.Has_unlock_items();
            }
        );
    }
}
