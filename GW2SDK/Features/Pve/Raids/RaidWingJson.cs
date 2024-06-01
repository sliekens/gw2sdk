using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidWingJson
{
    public static RaidWing GetRaidWing(this JsonElement json)
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
            Id = id.Map(static value => value.GetStringRequired()),
            Encounters = events.Map(
                static values => values.GetList(static value => value.GetEncounter())
            )
        };
    }
}
