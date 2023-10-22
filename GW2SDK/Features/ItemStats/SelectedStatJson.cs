using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.ItemStats;

internal static class SelectedStatJson
{
    public static SelectedStat GetSelectedStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember attributes = "attributes";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(attributes.Name))
            {
                attributes = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SelectedStat
        {
            Id = id.Map(value => value.GetInt32()),
            Attributes = attributes.Map(
                value => value.GetSelectedModification(missingMemberBehavior)
            )
        };
    }
}
