using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.BuildStorage;

[PublicAPI]
public static class BuildJson
{
    public static Build GetBuild(this JsonElement json, MissingMemberBehavior missingMemberBehavior)
    {
        RequiredMember<string> name = new("name");
        RequiredMember<ProfessionName> profession = new("profession");
        RequiredMember<Specialization> specializations = new("specializations");
        RequiredMember<SkillBar> skills = new("skills");
        RequiredMember<SkillBar> aquaticSkills = new("aquatic_skills");
        OptionalMember<PetSkillBar> pets = new("pets");
        OptionalMember<string?> legends = new("legends");
        OptionalMember<string?> aquaticLegends = new("aquatic_legends");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(profession.Name))
            {
                profession.Value = member.Value;
            }
            else if (member.NameEquals(specializations.Name))
            {
                specializations.Value = member.Value;
            }
            else if (member.NameEquals(skills.Name))
            {
                skills.Value = member.Value;
            }
            else if (member.NameEquals(aquaticSkills.Name))
            {
                aquaticSkills.Value = member.Value;
            }
            else if (member.NameEquals(pets.Name))
            {
                pets.Value = member.Value;
            }
            else if (member.NameEquals(legends.Name))
            {
                legends.Value = member.Value;
            }
            else if (member.NameEquals(aquaticLegends.Name))
            {
                aquaticLegends.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Build
        {
            Name = name.GetValue(),
            Profession = profession.GetValue(missingMemberBehavior),
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
