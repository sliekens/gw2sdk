using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class RefinementEctoplasmRecipeJson
{
    public static RefinementEctoplasmRecipe GetRefinementEctoplasmRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember outputItemId = new("output_item_id");
        RequiredMember outputItemCount = new("output_item_count");
        RequiredMember minRating = new("min_rating");
        RequiredMember timeToCraft = new("time_to_craft_ms");
        RequiredMember disciplines = new("disciplines");
        RequiredMember flags = new("flags");
        RequiredMember ingredients = new("ingredients");
        RequiredMember id = new("id");
        RequiredMember chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("RefinementEctoplasm"))
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

        return new RefinementEctoplasmRecipe
        {
            Id = id.Select(value => value.GetInt32()),
            OutputItemId = outputItemId.Select(value => value.GetInt32()),
            OutputItemCount = outputItemCount.Select(value => value.GetInt32()),
            MinRating = minRating.Select(value => value.GetInt32()),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.SelectMany(value => value.GetEnum<CraftingDisciplineName>(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<RecipeFlag>(missingMemberBehavior)),
            Ingredients =
                ingredients.SelectMany(value => value.GetIngredient(missingMemberBehavior)),
            ChatLink = chatLink.Select(value => value.GetStringRequired())
        };
    }
}
