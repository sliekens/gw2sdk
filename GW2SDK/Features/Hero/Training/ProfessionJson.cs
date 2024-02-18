using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class ProfessionJson
{
    public static Profession GetProfession(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember code = "code";
        RequiredMember icon = "icon";
        RequiredMember iconBig = "icon_big";
        RequiredMember specializations = "specializations";
        RequiredMember weapons = "weapons";
        RequiredMember flags = "flags";
        RequiredMember skills = "skills";
        RequiredMember training = "training";
        RequiredMember skillsByPalette = "skills_by_palette";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == code.Name)
            {
                code = member;
            }
            else if (member.Name == icon.Name)
            {
                icon = member;
            }
            else if (member.Name == iconBig.Name)
            {
                iconBig = member;
            }
            else if (member.Name == specializations.Name)
            {
                specializations = member;
            }
            else if (member.Name == weapons.Name)
            {
                weapons = member;
            }
            else if (member.Name == flags.Name)
            {
                flags = member;
            }
            else if (member.Name == skills.Name)
            {
                skills = member;
            }
            else if (member.Name == training.Name)
            {
                training = member;
            }
            else if (member.Name == skillsByPalette.Name)
            {
                skillsByPalette = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Profession
        {
            Id = id.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Name = name.Map(value => value.GetStringRequired()),
            Code = code.Map(value => value.GetInt32()),
            IconHref = icon.Map(value => value.GetStringRequired()),
            BigIconHref = iconBig.Map(value => value.GetStringRequired()),
            SpecializationIds =
                specializations.Map(values => values.GetList(value => value.GetInt32())),
            Weapons =
                weapons.Map(
                    value =>
                        value.GetMap(
                            GetWeaponType,
                            item => item.GetWeaponProficiency(missingMemberBehavior)
                        )
                ),
            Flags = flags.Map(values => values.GetProfessionFlags()),
            Skills =
                skills.Map(
                    values =>
                        values.GetList(value => value.GetSkillSummary(missingMemberBehavior))
                ),
            Training =
                training.Map(
                    values => values.GetList(value => value.GetTraining(missingMemberBehavior))
                ),
            SkillsByPalette =
                skillsByPalette.Map(value => value.GetSkillsByPalette(missingMemberBehavior))
        };

        static WeaponType GetWeaponType(string text)
        {
            // The old name for harpoon gun is used in the API
            if (string.Equals("Speargun", text, StringComparison.Ordinal))
            {
                return WeaponType.HarpoonGun;
            }
#if NET
            return Enum.Parse<WeaponType>(text);
#else
            return (WeaponType)Enum.Parse(typeof(WeaponType), text);
#endif
        }
    }
}
