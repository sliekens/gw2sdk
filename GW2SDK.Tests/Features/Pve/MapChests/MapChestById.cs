﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public class MapChestById
{
    [Theory]
    [InlineData("auric_basin_heros_choice_chest")]
    [InlineData("crystal_oasis_heros_choice_chest")]
    [InlineData("domain_of_vabbi_heros_choice_chest")]
    public async Task Can_be_found(string id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.MapChests.GetMapChestById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
