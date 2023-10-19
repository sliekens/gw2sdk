using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class GuildDecorationRecipeJson
{
    public static GuildDecorationRecipe GetGuildDecorationRecipe(
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
        OptionalMember guildIngredients = "guild_ingredients";
        RequiredMember outputUpgradeId = "output_upgrade_id";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildDecoration"))
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
            else if (member.NameEquals(guildIngredients.Name))
            {
                guildIngredients = member;
            }
            else if (member.NameEquals(outputUpgradeId.Name))
            {
                outputUpgradeId = member;
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

        return new GuildDecorationRecipe
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
            GuildIngredients =
                guildIngredients.SelectMany(
                    value => value.GetGuildIngredient(missingMemberBehavior)
                ),
            OutputUpgradeId = outputUpgradeId.Select(value => value.GetInt32()),
            ChatLink = chatLink.Select(value => value.GetStringRequired())
        };
    }
}
