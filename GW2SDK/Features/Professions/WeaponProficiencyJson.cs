using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Professions;

[PublicAPI]
public static class WeaponProficiencyJson
{
    public static WeaponProficiency GetWeaponProficiency(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember<int> specialization = new("specialization");
        RequiredMember<WeaponFlag> flags = new("flags");
        RequiredMember<WeaponSkill> skills = new("skills");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(specialization.Name))
            {
                specialization.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponProficiency
        {
            RequiredSpecialization = specialization.GetValue(),
            Flags = flags.GetValues(missingMemberBehavior),
            Skills = skills.SelectMany(value => value.GetWeaponSkill(missingMemberBehavior))
        };
    }
}
