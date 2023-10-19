using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class BuildJson
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember name = "name";
        RequiredMember profession = "profession";
        RequiredMember specializations = "specializations";
        RequiredMember skills = "skills";
        RequiredMember aquaticSkills = "aquatic_skills";
        OptionalMember pets = "pets";
        OptionalMember legends = "legends";
        OptionalMember aquaticLegends = "aquatic_legends";

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
            Name = name.Map(value => value.GetStringRequired()),
            Profession = profession.Map(value => value.GetEnum<ProfessionName>(missingMemberBehavior)),
            Specializations =
                specializations.Map(values => values.GetList(value => value.GetSpecialization(missingMemberBehavior))),
            Skills = skills.Map(value => value.GetSkillBar(missingMemberBehavior)),
            AquaticSkills = aquaticSkills.Map(value => value.GetSkillBar(missingMemberBehavior)),
            Pets = pets.Map(value => value.GetPetSkillBar(missingMemberBehavior)),
            Legends = legends.Map(values => values.GetList(value => value.GetString())),
            AquaticLegends = aquaticLegends.Map(values => values.GetList(value => value.GetString()))
        };
    }
}
