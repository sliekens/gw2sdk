using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class BuildJson
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = new("name");
        RequiredMember profession = new("profession");
        RequiredMember specializations = new("specializations");
        RequiredMember skills = new("skills");
        RequiredMember aquaticSkills = new("aquatic_skills");
        OptionalMember pets = new("pets");
        OptionalMember legends = new("legends");
        OptionalMember aquaticLegends = new("aquatic_legends");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession = member;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations = member;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills = member;
            }
            else if (member.NameEquals(aquaticSkills.Name))
            {
                aquaticSkills = member;
            }
            else if (member.NameEquals(pets.Name))
            {
                pets = member;
            }
            else if (member.NameEquals(legends.Name))
            {
                legends = member;
            }
            else if (member.NameEquals(aquaticLegends.Name))
            {
                aquaticLegends = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build
        {
            Name = name.Select(value => value.GetStringRequired()),
            Profession = profession.Select(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Specializations =
                specializations.SelectMany(value => value.GetSpecialization(missingMemberBehavior)),
            Skills = skills.Select(value => value.GetSkillBar(missingMemberBehavior)),
            AquaticSkills = aquaticSkills.Select(value => value.GetSkillBar(missingMemberBehavior)),
            Pets = pets.Select(value => value.GetPetSkillBar(missingMemberBehavior)),
            Legends = legends.SelectMany(value => value.GetString()),
            AquaticLegends = aquaticLegends.SelectMany(value => value.GetString())
        };
    }
}
