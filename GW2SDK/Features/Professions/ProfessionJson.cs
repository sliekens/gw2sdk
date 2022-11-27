using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Professions;

[PublicAPI]
public static class ProfessionJson
{
    public static Profession GetProfession(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<ProfessionName> id = new("id");
        RequiredMember<string> name = new("name");
        RequiredMember<int> code = new("code");
        RequiredMember<string> icon = new("icon");
        RequiredMember<string> iconBig = new("icon_big");
        RequiredMember<int> specializations = new("specializations");
        RequiredMember<IDictionary<string, WeaponProficiency>> weapons = new("weapons");
        RequiredMember<ProfessionFlag> flags = new("flags");
        RequiredMember<SkillReference> skills = new("skills");
        RequiredMember<Training> training = new("training");
        RequiredMember<Dictionary<int, int>> skillsByPalette = new("skills_by_palette");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(code.Name))
            {
                code.Value = member.Value;
            }
            else if (member.NameEquals(icon.Name))
            {
                icon.Value = member.Value;
            }
            else if (member.NameEquals(iconBig.Name))
            {
                iconBig.Value = member.Value;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations.Value = member.Value;
            }
            else if (member.NameEquals(weapons.Name))
            {
                weapons.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (member.NameEquals(training.Name))
            {
                training.Value = member.Value;
            }
            else if (member.NameEquals(skillsByPalette.Name))
            {
                skillsByPalette.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Profession
        {
            Id = id.GetValue(missingMemberBehavior),
            Name = name.GetValue(),
            Code = code.GetValue(),
            Icon = icon.GetValue(),
            IconBig = iconBig.GetValue(),
            Specializations = specializations.SelectMany(value => value.GetInt32()),
            Weapons =
                weapons.Select(
                    value => value.GetMap(item => item.GetWeaponProficiency(missingMemberBehavior))
                ),
            Flags = flags.GetValues(missingMemberBehavior),
            Skills = skills.SelectMany(value => value.GetSkillReference(missingMemberBehavior)),
            Training = training.SelectMany(value => value.GetTraining(missingMemberBehavior)),
            SkillsByPalette =
                skillsByPalette.Select(value => value.GetSkillsByPalette(missingMemberBehavior))
        };
    }
}
