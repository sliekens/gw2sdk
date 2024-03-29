﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponProficiencyJson
{
    public static WeaponProficiency GetWeaponProficiency(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        NullableMember specialization = "specialization";
        RequiredMember flags = "flags";
        RequiredMember skills = "skills";

        foreach (var member in json.EnumerateObject())
        {
            if (specialization.Match(member))
            {
                specialization = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (skills.Match(member))
            {
                skills = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WeaponProficiency
        {
            RequiredSpecialization = specialization.Map(value => value.GetInt32()),
            Flags = flags.Map(values => values.GetWeaponFlags()),
            Skills = skills.Map(
                values => values.GetList(value => value.GetWeaponSkill(missingMemberBehavior))
            )
        };
    }
}
