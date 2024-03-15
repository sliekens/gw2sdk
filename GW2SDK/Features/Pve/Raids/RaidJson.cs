using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidJson
{
    public static Raid GetRaid(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = "id";
        RequiredMember wings = "wings";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (wings.Match(member))
            {
                wings = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Raid
        {
            Id = id.Map(value => value.GetStringRequired()),
            Wings = wings.Map(
                values => values.GetList(value => value.GetRaidWing(missingMemberBehavior))
            )
        };
    }
}
