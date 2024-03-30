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
            if (id.Match(member))
            {
                id = member;
            }
            else if (name.Match(member))
            {
                name = member;
            }
            else if (code.Match(member))
            {
                code = member;
            }
            else if (icon.Match(member))
            {
                icon = member;
            }
            else if (iconBig.Match(member))
            {
                iconBig = member;
            }
            else if (specializations.Match(member))
            {
                specializations = member;
            }
            else if (weapons.Match(member))
            {
                weapons = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (skills.Match(member))
            {
                skills = member;
            }
            else if (training.Match(member))
            {
                training = member;
            }
            else if (skillsByPalette.Match(member))
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
            Id = id.Map(value => value.GetEnum<ProfessionName>()),
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

        static Extensible<WeaponType> GetWeaponType(string text)
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
