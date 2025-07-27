using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Pve.Raids;

internal static class EncounterJson
{
    public static Encounter GetEncounter(this in JsonElement json)
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Encounter
        {
            Id = id.Map(static (in JsonElement value) => value.GetStringRequired()),
            Kind = type.Map(static (in JsonElement value) => value.GetEnum<EncounterKind>())
        };
    }
}
