using System.Text.Json;

using GuildWars2.Hero.Builds;
using GuildWars2.Json;

namespace GuildWars2.Hero.Training;

internal static class ProfessionJson
{
    public static Profession GetProfession(this in JsonElement json)
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
        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        string iconString = icon.Map(static (in JsonElement value) => value.GetStringRequired());
        string iconBigString = iconBig.Map(static (in JsonElement value) => value.GetStringRequired());
        return new Profession
        {
            Id = id.Map(static (in JsonElement value) => value.GetEnum<ProfessionName>()),
            Name = name.Map(static (in JsonElement value) => value.GetStringRequired()),
            Code = code.Map(static (in JsonElement value) => value.GetInt32()),
#pragma warning disable CS0618 // Suppress obsolete warning for IconHref/BigIconHref assignment
            IconHref = iconString,
            BigIconHref = iconBigString,
#pragma warning restore CS0618
            IconUrl = new Uri(iconString, UriKind.RelativeOrAbsolute),
            BigIconUrl = new Uri(iconBigString, UriKind.RelativeOrAbsolute),
            SpecializationIds =
                specializations.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetInt32())
                ),
            Weapons =
                weapons.Map(static (in JsonElement value) => value.GetMap(
                        GetWeaponType,
                        static (in JsonElement item) => item.GetWeaponProficiency()
                    )
                ),
            Flags = flags.Map(static (in JsonElement values) => values.GetProfessionFlags()),
            Skills =
                skills.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetSkillSummary())
                ),
            Training =
                training.Map(static (in JsonElement values) => values.GetList(static (in JsonElement value) => value.GetTraining())),
            SkillsByPalette = skillsByPalette.Map(static (in JsonElement value) => value.GetSkillsByPalette())
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
