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

        foreach (JsonProperty member in json.EnumerateObject())
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
            RequiredSpecialization = specialization.Map(static (in value) => value.GetInt32()),
            Flags = flags.Map(static (in values) => values.GetWeaponFlags()),
            Skills = skills.Map(static (in values) =>
                values.GetList(static (in value) => value.GetWeaponSkill())
            )
        };
    }
}
