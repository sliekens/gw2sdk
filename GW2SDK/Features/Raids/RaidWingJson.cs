using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Raids;

[PublicAPI]
public static class RaidWingJson
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
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(events.Name))
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
            Id = id.Select(value => value.GetStringRequired()),
            Encounters = events.SelectMany(value => value.GetEncounter(missingMemberBehavior))
        };
    }
}
