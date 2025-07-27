using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidWingJson
{
    public static RaidWing GetRaidWing(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember events = "events";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (events.Match(member))
            {
                events = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new RaidWing
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Encounters = events.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetEncounter())
            )
        };
    }
}
