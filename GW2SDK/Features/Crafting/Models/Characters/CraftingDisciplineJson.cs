using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class CraftingDisciplineJson
{
    public static CraftingDiscipline GetCraftingDiscipline(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember discipline = new("discipline");
        RequiredMember rating = new("rating");
        RequiredMember active = new("active");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(discipline.Name))
            {
                discipline.Value = member.Value;
            }
            else if (member.NameEquals(rating.Name))
            {
                rating.Value = member.Value;
            }
            else if (member.NameEquals(active.Name))
            {
                active.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CraftingDiscipline
        {
            Discipline = discipline.Select(value => value.GetEnum<CraftingDisciplineName>(missingMemberBehavior)),
            Rating = rating.Select(value => value.GetInt32()),
            Active = active.Select(value => value.GetBoolean())
        };
    }
}
