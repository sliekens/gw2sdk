using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Raids;

[PublicAPI]
public static class RaidJson
{
    public static Raid GetRaid(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember id = new("id");
        RequiredMember wings = new("wings");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(wings.Name))
            {
                wings.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Raid
        {
            Id = id.Select(value => value.GetStringRequired()),
            Wings = wings.SelectMany(value => value.GetRaidWing(missingMemberBehavior))
        };
    }
}
