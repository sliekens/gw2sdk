using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Professions;

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
            Icon = icon.Map(value => value.GetStringRequired()),
            IconBig = iconBig.Map(value => value.GetStringRequired()),
            Specializations =
                specializations.Map(values => values.GetList(value => value.GetInt32())),
            Weapons =
                weapons.Map(
                    value =>
                        value.GetMap(item => item.GetWeaponProficiency(missingMemberBehavior))
                ),
            Flags =
                flags.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<ProfessionFlag>(missingMemberBehavior)
                        )
                ),
            Skills =
                skills.Map(
                    values =>
                        values.GetList(value => value.GetSkillReference(missingMemberBehavior))
                ),
            Training =
                training.Map(
                    values => values.GetList(value => value.GetTraining(missingMemberBehavior))
                ),
            SkillsByPalette =
                skillsByPalette.Map(value => value.GetSkillsByPalette(missingMemberBehavior))
        };
    }
}
