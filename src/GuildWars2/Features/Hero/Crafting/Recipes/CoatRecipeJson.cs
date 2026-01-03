using System.Text.Json;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class CoatRecipeJson
{
    public static CoatRecipe GetCoatRecipe(this in JsonElement json)
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
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Coat"))
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

        return new CoatRecipe
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            OutputItemId = outputItemId.Map(static (in value) => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(static (in value) => value.GetInt32()),
            MinRating = minRating.Map(static (in value) => value.GetInt32()),
            TimeToCraft =
                timeToCraft.Map(static (in value) => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetEnum<CraftingDisciplineName>())
                ),
            Flags = flags.Map(static (in values) => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(static (in values) =>
                    values.GetList(static (in value) => value.GetIngredient())
                ),
            ChatLink = chatLink.Map(static (in value) => value.GetStringRequired())
        };
    }
}
