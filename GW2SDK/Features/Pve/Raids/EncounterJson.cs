using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class EncounterJson
{
    public static Encounter GetEncounter(this JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember type = "type";

        foreach (var member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (type.Match(member))
            {
                type = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Encounter
        {
            Id = id.Map(static value => value.GetStringRequired()),
            Kind = type.Map(static value => value.GetEnum<EncounterKind>())
        };
    }
}
