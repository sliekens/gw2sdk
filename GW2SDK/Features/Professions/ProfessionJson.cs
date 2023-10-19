using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Professions;

[PublicAPI]
public static class ProfessionJson
{
    public static Profession GetProfession(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember name = new("name");
        RequiredMember code = new("code");
        RequiredMember icon = new("icon");
        RequiredMember iconBig = new("icon_big");
        RequiredMember specializations = new("specializations");
        RequiredMember weapons = new("weapons");
        RequiredMember flags = new("flags");
        RequiredMember skills = new("skills");
        RequiredMember training = new("training");
        RequiredMember skillsByPalette = new("skills_by_palette");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(code.Name))
            {
                code = member;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon = member;
            }
            else if (member.NameEquals(iconBig.Name))
            {
                iconBig = member;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations = member;
            }
            else if (member.NameEquals(weapons.Name))
            {
                weapons = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = member;
            }
            else if (member.NameEquals(training.Name))
            {
                training = member;
            }
            else if (member.NameEquals(skillsByPalette.Name))
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
            Id = id.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Name = name.Select(value => value.GetStringRequired()),
            Code = code.Select(value => value.GetInt32()),
            Icon = icon.Select(value => value.GetStringRequired()),
            IconBig = iconBig.Select(value => value.GetStringRequired()),
            Specializations = specializations.SelectMany(value => value.GetInt32()),
            Weapons =
                weapons.Select(
                    value => value.GetMap(item => item.GetWeaponProficiency(missingMemberBehavior))
                ),
            Flags = flags.SelectMany(value => value.GetEnum<ProfessionFlag>(missingMemberBehavior)),
            Skills = skills.SelectMany(value => value.GetSkillReference(missingMemberBehavior)),
            Training = training.SelectMany(value => value.GetTraining(missingMemberBehavior)),
            SkillsByPalette =
                skillsByPalette.Select(value => value.GetSkillsByPalette(missingMemberBehavior))
        };
    }
}
