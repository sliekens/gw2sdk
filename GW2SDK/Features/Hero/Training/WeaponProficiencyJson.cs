using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponProficiencyJson
{
    public static WeaponProficiency GetWeaponProficiency(this JsonElement json)
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new WeaponProficiency
        {
            RequiredSpecialization = specialization.Map(static value => value.GetInt32()),
            Flags = flags.Map(static values => values.GetWeaponFlags()),
            Skills = skills.Map(
                static values => values.GetList(static value => value.GetWeaponSkill())
            )
        };
    }
}
