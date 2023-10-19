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
        NullableMember specialization = new("specialization");
        RequiredMember flags = new("flags");
        RequiredMember skills = new("skills");

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
            RequiredSpecialization = specialization.Select(value => value.GetInt32()),
            Flags = flags.SelectMany(value => value.GetEnum<WeaponFlag>(missingMemberBehavior)),
            Skills = skills.SelectMany(value => value.GetWeaponSkill(missingMemberBehavior))
        };
    }
}
