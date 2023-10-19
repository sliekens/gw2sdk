using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class LearnedCraftingDisciplinesJson
{
    public static LearnedCraftingDisciplines GetLearnedCraftingDisciplines(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember crafting = new("crafting");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(crafting.Name))
            {
                crafting.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LearnedCraftingDisciplines
        {
            Disciplines = crafting.SelectMany(
                value => value.GetCraftingDiscipline(missingMemberBehavior)
            )
        };
    }
}
