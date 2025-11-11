using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Inventories;

[ServiceDataSource]
public class SharedInventory(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Inventories.Inventory actual, _) = await sut.Hero.Inventory.GetSharedInventory(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual.Items);
        Assert.All(actual.Items, slot =>
        {
            if (slot is not null)
            {
                Assert.True(slot.Id > 0);
                Assert.True(slot.Count > 0);
            }
        });
    }
}
