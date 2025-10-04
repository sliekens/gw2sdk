using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Disciplines;

internal static class CraftingDisciplineJson
{
    public static CraftingDiscipline GetCraftingDiscipline(this in JsonElement json)
    {
        RequiredMember discipline = "discipline";
        RequiredMember rating = "rating";
        RequiredMember active = "active";

        foreach (JsonProperty member in json.EnumerateObject())
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new CraftingDiscipline
        {
            Discipline =
                discipline.Map(static (in value) => value.GetEnum<CraftingDisciplineName>()),
            Rating = rating.Map(static (in value) => value.GetInt32()),
            Active = active.Map(static (in value) => value.GetBoolean())
        };
    }
}
