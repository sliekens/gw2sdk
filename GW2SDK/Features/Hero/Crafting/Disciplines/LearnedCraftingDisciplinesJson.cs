using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal static class LearnedCraftingDisciplinesJson
{
    public static LearnedCraftingDisciplines GetLearnedCraftingDisciplines(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember crafting = "crafting";

        foreach (var member in json.EnumerateObject())
        {
            if (crafting.Match(member))
            {
                crafting = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LearnedCraftingDisciplines
        {
            Disciplines = crafting.Map(
                values => values.GetList(
                    value => value.GetCraftingDiscipline(missingMemberBehavior)
                )
            )
        };
    }
}
