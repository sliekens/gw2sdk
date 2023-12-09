using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class Recipes
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach (var (actual, context) in sut.Hero.Crafting.Recipes.GetRecipesBulk(degreeOfParallelism: 3).Take(600))
        {
            actual.Has_id();
            actual.Has_output_item_id();
            actual.Has_item_count();
            actual.Has_min_rating_between_0_and_500();
            actual.Has_time_to_craft();
            Assert.NotNull(context);
        }
    }
}
