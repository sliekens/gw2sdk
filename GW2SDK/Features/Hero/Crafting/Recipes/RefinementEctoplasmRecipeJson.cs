using System.Text.Json;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class RefinementEctoplasmRecipeJson
{
    public static RefinementEctoplasmRecipe GetRefinementEctoplasmRecipe(this in JsonElement json)
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
                if (!member.Value.ValueEquals("RefinementEctoplasm"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (outputItemId.Match(member))
            {
                outputItemId = member;
            }
            else if (outputItemCount.Match(member))
            {
                outputItemCount = member;
            }
            else if (minRating.Match(member))
            {
                minRating = member;
            }
            else if (timeToCraft.Match(member))
            {
                timeToCraft = member;
            }
            else if (disciplines.Match(member))
            {
                disciplines = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (ingredients.Match(member))
            {
                ingredients = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new RefinementEctoplasmRecipe
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            OutputItemId = outputItemId.Map(static (in JsonElement value) => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(static (in JsonElement value) => value.GetInt32()),
            MinRating = minRating.Map(static (in JsonElement value) => value.GetInt32()),
            TimeToCraft =
                timeToCraft.Map(static (in JsonElement value) => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<CraftingDisciplineName>())
                ),
            Flags = flags.Map(static (in JsonElement values) => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetIngredient())
                ),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}
