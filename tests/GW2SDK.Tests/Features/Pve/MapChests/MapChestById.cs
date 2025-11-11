using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class MapChestById(Gw2Client sut)
{
    [Test]
    [Arguments("auric_basin_heros_choice_chest")]
    [Arguments("crystal_oasis_heros_choice_chest")]
    [Arguments("domain_of_vabbi_heros_choice_chest")]
    public async Task Can_be_found(string id)
    {
        (MapChest actual, MessageContext context) = await sut.Pve.MapChests.GetMapChestById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
