using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesByOutputItemByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int ironIngot = 19683;
        const int pageSize = 3;
        (IImmutableValueSet<Recipe> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesByOutputItemIdByPage(ironIngot, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        const int ironIngotRecipe = 19;
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(1);
        await Assert.That(context.PageTotal).IsEqualTo(1);
        await Assert.That(context.ResultTotal).IsEqualTo(1);
        await Assert.That(actual.Count).IsEqualTo(1);
        Recipe? found = actual.Single();
        await Assert.That(found.Id).IsEqualTo(ironIngotRecipe);
    }
}
