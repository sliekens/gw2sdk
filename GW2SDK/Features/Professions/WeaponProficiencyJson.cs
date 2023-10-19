using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class WeaponProficiencyJson
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
            if (member.NameEquals(specialization.Name))
            {
                specialization = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(skills.Name))
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
