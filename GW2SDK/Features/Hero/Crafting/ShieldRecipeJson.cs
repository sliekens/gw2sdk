using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal static class ShieldRecipeJson
{
    public static ShieldRecipe GetShieldRecipe(
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
                if (!member.Value.ValueEquals("Shield"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == outputItemId.Name)
            {
                outputItemId = member;
            }
            else if (member.Name == outputItemCount.Name)
            {
                outputItemCount = member;
            }
            else if (member.Name == minRating.Name)
            {
                minRating = member;
            }
            else if (member.Name == minRating.Name)
            {
                minRating = member;
            }
            else if (member.Name == timeToCraft.Name)
            {
                timeToCraft = member;
            }
            else if (member.Name == disciplines.Name)
            {
                disciplines = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == ingredients.Name)
            {
                ingredients = member;
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == chatLink.Name)
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ShieldRecipe
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
