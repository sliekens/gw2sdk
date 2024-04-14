using System.Text.Json;
using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class ProfessionJson
{
    public static Profession GetProfession(
        this JsonElement json
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Profession
        {
            Id = id.Map(static value => value.GetEnum<ProfessionName>()),
            Name = name.Map(static value => value.GetStringRequired()),
            Code = code.Map(static value => value.GetInt32()),
            IconHref = icon.Map(static value => value.GetStringRequired()),
            BigIconHref = iconBig.Map(static value => value.GetStringRequired()),
            SpecializationIds =
                specializations.Map(static values => values.GetList(static value => value.GetInt32())),
            Weapons =
                weapons.Map(static value =>
                        value.GetMap(
                            GetWeaponType,
                            item => item.GetWeaponProficiency()
                        )
                ),
            Flags = flags.Map(static values => values.GetProfessionFlags()),
            Skills =
                skills.Map(static values =>
                        values.GetList(static value => value.GetSkillSummary())
                ),
            Training =
                training.Map(static values => values.GetList(static value => value.GetTraining())
                ),
            SkillsByPalette =
                skillsByPalette.Map(static value => value.GetSkillsByPalette())
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
