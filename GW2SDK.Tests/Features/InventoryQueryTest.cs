using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features;

public class InventoryQueryTest
{
    [Fact]
    public async Task Shared_inventory_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<InventoryQuery>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.GetSharedInventory(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(
            actual.Value,
            slot =>
            {
                if (slot is not null)
                {
                    Assert.True(slot.Id > 0);
                    Assert.True(slot.Count > 0);
                }
            }
            );
    }
}
