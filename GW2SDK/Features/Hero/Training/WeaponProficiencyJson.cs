using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class WeaponProficiencyJson
{
    public static WeaponProficiency GetWeaponProficiency(this in JsonElement json)
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
            RequiredSpecialization = specialization.Map(static (in JsonElement value) => value.GetInt32()),
            Flags = flags.Map(static (in JsonElement values) => values.GetWeaponFlags()),
            Skills = skills.Map(static (in JsonElement values) =>
                values.GetList(static (in JsonElement value) => value.GetWeaponSkill())
            )
        };
    }
}
