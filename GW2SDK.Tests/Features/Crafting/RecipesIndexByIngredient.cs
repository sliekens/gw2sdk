﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Crafting;

public class RecipesIndexByIngredient
{
    [Fact]
    public async Task Can_be_found()
    {
        // Normally the limit for ids=all is 200 items
        //   but that doesn't seem to apply for recipes search by input/output item
        // There are 800+ recipes that require a vision crystal
        var sut = Composer.Resolve<Gw2Client>();

        const int visionCrystal = 46746;
        var actual = await sut.Crafting.GetRecipesIndexByIngredientItemId(visionCrystal);

        Assert.NotInRange(actual.Value.Count, 0, 200); // Greater than 200
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Context.ResultContext.ResultCount);
    }
}
