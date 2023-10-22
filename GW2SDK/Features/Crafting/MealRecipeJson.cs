using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class MealRecipeJson
{
    public static MealRecipe GetMealRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember outputItemId = "output_item_id";
        RequiredMember outputItemCount = "output_item_count";
        RequiredMember minRating = "min_rating";
        RequiredMember timeToCraft = "time_to_craft_ms";
        RequiredMember disciplines = "disciplines";
        RequiredMember flags = "flags";
        RequiredMember ingredients = "ingredients";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Meal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = member;
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = member;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = member;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = member;
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = member;
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = member;
            }
            else if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MealRecipe
        {
            Id = id.Map(value => value.GetInt32()),
            OutputItemId = outputItemId.Map(value => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(value => value.GetInt32()),
            MinRating = minRating.Map(value => value.GetInt32()),
            TimeToCraft = timeToCraft.Map(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<CraftingDisciplineName>(missingMemberBehavior)
                        )
                ),
            Flags =
                flags.Map(
                    values => values.GetList(
                        value => value.GetEnum<RecipeFlag>(missingMemberBehavior)
                    )
                ),
            Ingredients =
                ingredients.Map(
                    values => values.GetList(value => value.GetIngredient(missingMemberBehavior))
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}
