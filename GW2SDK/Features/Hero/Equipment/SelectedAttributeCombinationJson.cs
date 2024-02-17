using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment;

internal static class SelectedAttributeCombinationJson
{
    public static SelectedAttributeCombination GetSelectedAttributeCombination(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember attributes = "attributes";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == attributes.Name)
            {
                attributes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedAttributeCombination
        {
            Id = id.Map(value => value.GetInt32()),
            Attributes = attributes.Map(value => value.GetAttributes(missingMemberBehavior))
        };
    }
}
