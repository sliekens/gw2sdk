using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal static class LearnedCraftingDisciplinesJson
{
    public static LearnedCraftingDisciplines GetLearnedCraftingDisciplines(this JsonElement json)
    {
        RequiredMember crafting = "crafting";

        foreach (var member in json.EnumerateObject())
        {
            if (crafting.Match(member))
            {
                crafting = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new LearnedCraftingDisciplines
        {
            Disciplines = crafting.Map(static values =>
                values.GetList(static value => value.GetCraftingDiscipline())
            )
        };
    }
}
