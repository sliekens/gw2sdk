using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class RaidWingJson
{
    public static RaidWing GetRaidWing(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
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
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RaidWing
        {
            Id = id.Map(value => value.GetStringRequired()),
            Encounters = events.Map(
                values => values.GetList(value => value.GetEncounter(missingMemberBehavior))
            )
        };
    }
}
