using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class PetSkillBarJson
{
    public static PetSkillBar GetPetSkillBar(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int?> terrestrial = new("terrestrial");
        RequiredMember<int?> aquatic = new("aquatic");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(terrestrial.Name))
            {
                terrestrial.Value = member.Value;
            }
            else if (member.NameEquals(aquatic.Name))
            {
                aquatic.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PetSkillBar
        {
            Terrestrial =
                terrestrial.SelectMany(
                    value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
                ),
            Aquatic = aquatic.SelectMany(
                value => value.ValueKind == JsonValueKind.Null ? null : value.GetInt32()
            )
        };
    }
}
