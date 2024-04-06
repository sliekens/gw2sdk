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

                Assert.True(actual.Id > 0);
                Assert.True(actual.OutputItemId >= 1);
                Assert.True(actual.OutputItemCount >= 1);
                Assert.InRange(actual.MinRating, 0, 500);
                Assert.True(actual.TimeToCraft.Ticks >= 1);
                Assert.All(actual.Disciplines, discipline => Assert.True(discipline.IsDefined()));
                Assert.Empty(actual.Flags.Other);

                Assert.All(
                    actual.Ingredients,
                    ingredient =>
                    {
                        Assert.True(ingredient.Kind.IsDefined());
                        Assert.True(ingredient.Id > 0);
                        Assert.True(ingredient.Count > 0);
                    }
                );

                var chatLink = actual.GetChatLink();
                Assert.Equal(actual.Id, chatLink.RecipeId);
                Assert.Equal(actual.ChatLink, chatLink.ToString());
            }
        );
    }
}
