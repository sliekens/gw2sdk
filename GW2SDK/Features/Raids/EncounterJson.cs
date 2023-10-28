using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Raids;

internal static class EncounterJson
{
    public static Encounter GetEncounter(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember type = "type";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == type.Name)
            {
                type = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Encounter
        {
            Id = id.Map(value => value.GetStringRequired()),
            Kind = type.Map(value => value.GetEnum<EncounterKind>(missingMemberBehavior))
        };
    }
}
