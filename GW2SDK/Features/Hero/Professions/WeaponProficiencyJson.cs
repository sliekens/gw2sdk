using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Professions;

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
            if (member.Name == specialization.Name)
            {
                specialization = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == skills.Name)
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
            Flags = flags.Map(
                values =>
                    values.GetList(value => value.GetEnum<WeaponFlag>(missingMemberBehavior))
            ),
            Skills = skills.Map(
                values => values.GetList(value => value.GetWeaponSkill(missingMemberBehavior))
            )
        };
    }
}
