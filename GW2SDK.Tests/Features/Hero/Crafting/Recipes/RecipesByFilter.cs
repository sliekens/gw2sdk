using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesByFilter
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

        var (actual, _) = await sut.Hero.Crafting.Recipes.GetRecipesByIds(ids);

        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_output_item_id();
                entry.Has_item_count();
                entry.Has_min_rating_between_0_and_500();
                entry.Has_time_to_craft();
            }
        );
    }
}
