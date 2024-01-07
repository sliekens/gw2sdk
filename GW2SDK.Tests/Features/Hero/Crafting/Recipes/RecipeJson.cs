using System.Text.Json;
using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipeJson
{
    [Fact]
    public void Recipes_can_be_created_from_json()
    {
        var recipes = FlatFileReader.Read("Data/recipes.json.gz").ToList();
        Assert.All(
            recipes,
            json =>
            {
                using var document = JsonDocument.Parse(json);
                var actual = document.RootElement.GetRecipe(MissingMemberBehavior.Error);

                actual.Has_id();
                actual.Has_output_item_id();
                actual.Has_item_count();
                actual.Has_min_rating_between_0_and_500();
                actual.Has_time_to_craft();

                var chatLink = actual.GetChatLink();
                Assert.Equal(actual.Id, chatLink.RecipeId);
                Assert.Equal(actual.ChatLink, chatLink.ToString());
            }
        );
    }
}
