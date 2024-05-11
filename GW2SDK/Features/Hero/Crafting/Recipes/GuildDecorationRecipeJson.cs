using System.Text.Json;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class GuildDecorationRecipeJson
{
    public static GuildDecorationRecipe GetGuildDecorationRecipe(this JsonElement json)
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
            else if (guildIngredients.Match(member))
            {
                guildIngredients = member;
            }
            else if (outputUpgradeId.Match(member))
            {
                outputUpgradeId = member;
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
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildDecorationRecipe
        {
            Id = id.Map(static value => value.GetInt32()),
            OutputItemId = outputItemId.Map(static value => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(static value => value.GetInt32()),
            MinRating = minRating.Map(static value => value.GetInt32()),
            TimeToCraft =
                timeToCraft.Map(static value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(
                    static values =>
                        values.GetList(static value => value.GetEnum<CraftingDisciplineName>())
                ),
            Flags = flags.Map(static values => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(
                    static values => values.GetList(static value => value.GetIngredient())
                ),
            GuildIngredients =
                guildIngredients.Map(
                    static values => values.GetList(static value => value.GetGuildIngredient())
                )
                ?? Empty.List<GuildIngredient>(),
            OutputUpgradeId = outputUpgradeId.Map(static value => value.GetInt32()),
            ChatLink = chatLink.Map(static value => value.GetStringRequired())
        };
    }
}
