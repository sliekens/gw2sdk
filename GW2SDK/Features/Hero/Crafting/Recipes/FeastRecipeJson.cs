using System.Text.Json;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class FeastRecipeJson
{
    public static FeastRecipe GetFeastRecipe(
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
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Feast"))
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
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new FeastRecipe
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
            Flags = flags.Map(values => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(
                    values => values.GetList(value => value.GetIngredient(missingMemberBehavior))
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}
