using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidJson
{
    public static Raid GetRaid(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Raid
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Wings = wings.Map(static values => values.GetList(static value => value.GetRaidWing())
            )
        };
    }
}
