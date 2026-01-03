using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidJson
{
    public static Raid GetRaid(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember wings = "wings";

        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Raid
        {
            Id = id.Map(static (in value) => value.GetStringRequired()),
            Wings = wings.Map(static (in values) => values.GetList(static (in value) => value.GetRaidWing()))
        };
    }
}
