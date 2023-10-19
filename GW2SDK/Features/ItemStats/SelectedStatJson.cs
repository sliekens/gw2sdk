using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.ItemStats;

[PublicAPI]
public static class SelectedStatJson
{
    public static SelectedStat GetSelectedStat(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember attributes = new("attributes");
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
            Id = id.Select(value => value.GetInt32()),
            Attributes =
                attributes.Select(value => value.GetSelectedModification(missingMemberBehavior))
        };
    }
}
