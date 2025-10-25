using GuildWars2.Pve.MapChests;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public class MapChestById
{
    [Test]
    [Arguments("auric_basin_heros_choice_chest")]
    [Arguments("crystal_oasis_heros_choice_chest")]
    [Arguments("domain_of_vabbi_heros_choice_chest")]
    public async Task Can_be_found(string id)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (MapChest actual, MessageContext context) = await sut.Pve.MapChests.GetMapChestById(id, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
