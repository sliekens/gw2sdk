using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal static class CraftingDisciplineJson
{
    public static CraftingDiscipline GetCraftingDiscipline(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember discipline = "discipline";
        RequiredMember rating = "rating";
        RequiredMember active = "active";

        foreach (var member in json.EnumerateObject())
        {
            if (discipline.Match(member))
            {
                discipline = member;
            }
            else if (rating.Match(member))
            {
                rating = member;
            }
            else if (active.Match(member))
            {
                active = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CraftingDiscipline
        {
            Discipline =
                discipline.Map(
                    value => value.GetEnum<CraftingDisciplineName>(missingMemberBehavior)
                ),
            Rating = rating.Map(value => value.GetInt32()),
            Active = active.Map(value => value.GetBoolean())
        };
    }
}
