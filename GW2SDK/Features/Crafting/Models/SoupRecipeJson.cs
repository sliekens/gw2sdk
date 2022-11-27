using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
public static class SoupRecipeJson
{
    public static SoupRecipe GetSoupRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Soup"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId.Value = member.Value;
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount.Value = member.Value;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating.Value = member.Value;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating.Value = member.Value;
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft.Value = member.Value;
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SoupRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => value.GetIngredient(missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}
